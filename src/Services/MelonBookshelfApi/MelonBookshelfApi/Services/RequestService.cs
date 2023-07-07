using AutoMapper;
using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookchelfApi.Infrastructure.Data.Models.Enums;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.CustomObjects;
using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.RequestDtos;
using MelonBookshelfApi.ResponceModels;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MelonBookshelfApi.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public RequestService(IRepository repository, ILogger<RequestService> logger, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResourceRequestDto> GetRequestById(int requestId)
        {
            var request = await _repository.GetByIdAsync<Request>(requestId);

            return _mapper.Map<ResourceRequestDto>(request);
        }

        public IEnumerable<ResourceRequestDto> GetRequests()
        {
            var request = _repository.All<Request>().AsEnumerable();

            return _mapper.Map<IEnumerable<ResourceRequestDto>>(request);
        }

        public async Task<IEnumerable<UserRequestedResourceModel>> GetRequestsByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid User");
            }

            var requests = _repository.All<Request>().AsNoTracking().Where(a => a.UserID == userId).ToList();

            var model = new List<UserRequestedResourceModel>();
            foreach (var item in requests)
            {
                var followersCollection = _repository
                    .All<RequestFollower>()
                    .AsNoTracking()
                    .Where(a => a.RequestId == item.Id)
                    .Include(a => a.IdentityUser)
                    .Select(a => new User { Id = a.IdentityUser.Id, Name = a.IdentityUser.UserName })
                    .ToList();

                var upvotersCollection = _repository
                    .All<RequestFollower>()
                    .AsNoTracking()
                    .Where(a => a.RequestId == item.Id)
                    .Include(a => a.IdentityUser)
                    .Select(a => new User { Id = a.IdentityUser.Id, Name = a.IdentityUser.UserName })
                    .ToList();

                model.Add(new UserRequestedResourceModel
                {
                    Author = item.Author,
                    Category = item.Category,
                    Priority = item.Priority,
                    Status = item.Status,
                    Title = item.Title,
                    FollowersCollection = followersCollection,
                    FollowersCount = followersCollection.Count,
                    UpvotersCollection = upvotersCollection,
                    UpvotersCount = upvotersCollection.Count
                });
            }

            return model;
        }

        public async Task<ProcessRequestResult> ProcessRequestAsync(ResourceRequestDto requestDto, string userId)
        {
            var contentAvalability = await IsContentAvalable(requestDto.Title, requestDto.Author);

            switch (contentAvalability.ProcessRequest)
            {
                case ProcessRequest.ContentDoesNotExist:
                    break;
                case ProcessRequest.ContentRequestInProgress:
                    if(contentAvalability.RequestId == null)
                    {
                        throw new ArgumentException(ProcessRequest.UnableToProcessRequest.ToString());
                    }

                    int id = contentAvalability.RequestId.Value;

                    return new ProcessRequestResult 
                    { 
                        ProcessRequest = ProcessRequest.ContentRequestInProgress, 
                        RequestId = id
                    };
                case ProcessRequest.ContentAlreadyExists:
                    return new ProcessRequestResult { ProcessRequest = ProcessRequest.ContentAlreadyExists };
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return new ProcessRequestResult { ProcessRequest = ProcessRequest.UnableToProcessRequest };
            }

            var resourceRequest = new Request
            {
                Title = requestDto.Title,
                Type = requestDto.Type,
                Author = requestDto.Author,
                Description = requestDto.Description,
                Category = requestDto.Category,
                Priority = requestDto.Priority,
                Justification = requestDto.Justification,
                ConfirmationDate = DateTime.UtcNow,
                UserID = userId,
                Status = RequestStatus.Processing.ToString(),
                Link = requestDto.Link
            };

            await _repository.AddAsync(resourceRequest);
            await _repository.SaveChangesAsync();

            //SendConfirmationEmailToUser(resourceRequest);
            //SendConfirmationEmailToAdmin(resourceRequest);

            return new ProcessRequestResult { ProcessRequest = ProcessRequest.RequestProcessedSuccessfuly };
        }

        private async Task<ProcessRequestResult> IsContentAvalable(string title, string author)
        {
            var isAvalableInResources = await _repository
                .All<Resource>()
                .Where(a => a.Author.ToLower().Contains(author.ToLower()) && a.Title.ToLower().Contains(title.ToLower()))
                .FirstOrDefaultAsync();

            if(isAvalableInResources == null)
            {
                var isAvalableInRequests = await _repository
                .All<Request>()
                .Where(a => a.Author.ToLower().Contains(author.ToLower()) && a.Title.ToLower().Contains(title.ToLower()))
                .FirstOrDefaultAsync();

                if(isAvalableInRequests == null)
                {
                    return new ProcessRequestResult { ProcessRequest = ProcessRequest.ContentDoesNotExist};
                }

                return new ProcessRequestResult { ProcessRequest = ProcessRequest.ContentRequestInProgress, RequestId = isAvalableInRequests.Id };
            }

            return new ProcessRequestResult { ProcessRequest = ProcessRequest.ContentAlreadyExists };
        }
    }
}
