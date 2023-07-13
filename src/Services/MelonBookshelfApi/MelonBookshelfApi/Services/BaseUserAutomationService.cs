using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookchelfApi.Infrastructure.Data.Models.Enums;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MelonBookshelfApi.Services
{
	public class BaseUserAutomationService : IBaseUserAutomationService
	{
		private readonly IRepository _repository;
		private readonly ILogger _logger;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IMessageSender _messageSender;
		public BaseUserAutomationService(IRepository repository, ILogger<BaseUserAutomationService> logger, UserManager<IdentityUser> userManager, IMessageSender messageSender)
		{
			_repository = repository;
			_logger = logger;
			_userManager = userManager;
			_messageSender = messageSender;
		}

		public async Task FollowRequest(int requestId, string userId)
		{
			var request = await _repository.GetByIdAsync<Request>(requestId);

			var user = await _userManager.FindByIdAsync(userId);

			if (request == null)
			{
				throw new InvalidOperationException("The request is null");
			}
			if (user == null)
			{
				throw new ArgumentException("User is null");
			}

			if (request.UserID != userId)
			{
				var requestFollower = new RequestFollower
				{
					RequestId = requestId,
					UserID = userId
				};

				var follower = await _repository.All<RequestFollower>().Where(a => a.UserID == userId && a.RequestId == requestId).FirstOrDefaultAsync();

				if (follower != null)
				{
					await _repository.DeleteAsync<RequestFollower>(follower.RequestId, follower.UserID);
				}
				else
				{
					await _repository.AddAsync(requestFollower);
				}

				await _repository.SaveChangesAsync();
			}


		}

		public async Task ReturnPhysicalResource(int resourceId, string userId)
		{
			var resource = await _repository.GetByIdAsync<Resource>(resourceId);

			var user = await _userManager.FindByIdAsync(userId);

			if (resource == null)
			{
				throw new InvalidOperationException("The request is null");
			}
			if (user == null)
			{
				throw new ArgumentException("User is null");
			}

			var userResource = await _repository.All<UserResource>().Where(a => a.UserId == userId && a.ResourceId == resourceId).FirstOrDefaultAsync();

			if (userResource == null)
			{
				throw new InvalidOperationException("ResourceDoeseNotBelongThoTheUser");
			}

			resource.Status = ResourceStatus.Avalable.ToString();

			await _messageSender.SendMessage(user.Email, $"Resource Returned - You have successfully returned {resource.Title}!");

			_repository.Update(resource);
			await _repository.DeleteAsync<UserResource>(userResource.ResourceId, userResource.UserId);
			await _repository.SaveChangesAsync();
		}

		public async Task GetPhysicalResource(int resourceId, string userId)
		{
			var resource = await _repository.GetByIdAsync<Resource>(resourceId);

			var user = await _userManager.FindByIdAsync(userId);

			if (resource == null)
			{
				throw new InvalidOperationException("The request is null");
			}
			if (user == null)
			{
				throw new ArgumentException("User is null");
			}

			var userResource = new UserResource
			{
				UserId = user.Id,
				ResourceId = resourceId
			};

			resource.Status = ResourceStatus.Taken.ToString();

			await _messageSender.SendMessage(user.Email, $"Resource Taken - You have successfully taken {resource.Title}!");

			_repository.Update(resource);
			await _repository.AddAsync(userResource);
			await _repository.SaveChangesAsync();
		}

		public async Task UpvoteRequest(int requestId, string userId)
		{
			var request = await _repository.GetByIdAsync<Request>(requestId);

			var user = await _userManager.FindByIdAsync(userId);

			if (request == null)
			{
				throw new InvalidOperationException("The request is null");
			}
			if (user == null)
			{
				throw new ArgumentException("User is null");
			}

			if (request.UserID != userId)
			{
				var requestUpvoter = new RequestUpvoter
				{
					RequestId = requestId,
					UserID = userId
				};

				var upvoter = await _repository.All<RequestUpvoter>().Where(a => a.UserID == userId && a.RequestId == requestId).FirstOrDefaultAsync();

				if (upvoter != null)
				{
					try
					{
						await _repository.DeleteAsync<RequestUpvoter>(upvoter.RequestId, upvoter.UserID);

					}
					catch(Exception ex) { }
				}
				else
				{
					await _repository.AddAsync(requestUpvoter);
				}

				await _repository.SaveChangesAsync();
			}
		}
	}
}
