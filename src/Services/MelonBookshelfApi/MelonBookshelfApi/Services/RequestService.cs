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
using Org.BouncyCastle.Asn1.Cmp;

namespace MelonBookshelfApi.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMessageSender _messageSender;

        public RequestService(IMessageSender messageSender, IRepository repository, ILogger<RequestService> logger, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _messageSender = messageSender;
        }

        public async Task<ResourceRequestDto> GetRequestById(int requestId)
        {
            var request = await _repository.GetByIdAsync<Request>(requestId);

            var user = await _userManager.FindByIdAsync(request.UserID);

            var map = _mapper.Map<ResourceRequestDto>(request);

            map.UserName = user.UserName;

            return map;
        }

        public async Task<IEnumerable<ResourceRequestDto>> GetRequests()
        {
            var request = await _repository.All<Request>().AsNoTracking().ToListAsync();

            var map = _mapper.Map<IEnumerable<ResourceRequestDto>>(request).ToList();

            for (int i = 0; i < request.Count; i++) 
            {
                var user = await _userManager.FindByIdAsync(request[i].UserID);

                map[i].UserName = user.UserName;
            }

            return map;
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
                    Id = item.Id,
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
                Id = requestDto.Id,
                Title = requestDto.Title,
                Type = requestDto.Type,
                DeliveryStatus = requestDto.DeliveryStatus,
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

            await _messageSender.SendMessage(user.Email, $"Request Approved - Your request for {resourceRequest.Title} has been approved!");

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
