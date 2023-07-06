using AutoMapper;
using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookchelfApi.Infrastructure.Data.Models.Enums;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.RequestModels;
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

        public async Task<IEnumerable<ResourceRequestDto>> GetRequestsByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid User");
            }

            var requests = _repository.All<Request>().Where(a => a.UserID == userId);

            var map = _mapper.Map<IEnumerable<ResourceRequestDto>>(requests);

            return map;
        }

        public async Task<ProcessRequestResult> ProcessRequestAsync(ResourceRequestDto requestDto, string userId)
        {
            // Check if the title or author already exist or have been requested
            //bool titleExists = await _requestRepository.DoesTitleExistAsync(requestDto.Title);
            //bool authorExists = await _requestRepository.DoesAuthorExistAsync(requestDto.Author);

            //if (titleExists || authorExists)
            //{
            //    // Handle the case where title or author already exist or have been requested
            //    // You can return an appropriate response or provide support for the existing request
            //}

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return ProcessRequestResult.UnableToProcessRequest;
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

            // Save the resource request to the database
            await _repository.AddAsync(resourceRequest);
            await _repository.SaveChangesAsync();

            // Send confirmation emails to the user and admin
            //SendConfirmationEmailToUser(resourceRequest);
            //SendConfirmationEmailToAdmin(resourceRequest);

            return ProcessRequestResult.RequestProcessedSuccessfuly;
        }
    }
}
