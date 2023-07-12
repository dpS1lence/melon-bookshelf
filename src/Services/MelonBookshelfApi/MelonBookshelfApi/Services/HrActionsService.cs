using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookchelfApi.Infrastructure.Data.Models.Enums;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using ResourceStatus = MelonBookshelfApi.CustomObjects.Enums.ResourceStatus;

namespace MelonBookshelfApi.Services
{
    public class HrActionsService : IHrActionsService
    {
        private readonly IRepository _repository;

        public HrActionsService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Confirm(int requestId)
        {
            var request = await _repository.GetByIdAsync<Request>(requestId);

            if (request == null)
            {
                throw new ArgumentException(null, nameof(requestId));
            }

            request.ConfirmationDate = DateTime.Now;
            request.Status = RequestStatus.Processing.ToString();

            _repository.Update(request);
            await _repository.SaveChangesAsync();
        }

        public async Task Reject(int requestId, string justification)
        {
            var request = await _repository.GetByIdAsync<Request>(requestId);

            if (request == null)
            {
                throw new ArgumentException(null, nameof(requestId));
            }

            request.ConfirmationDate = DateTime.Now;
            request.Status = RequestStatus.Declined.ToString();
            request.Justification = justification;

            _repository.Update(request);
            await _repository.SaveChangesAsync();
        }

        public async Task SetRequestDeliveryStatus(int requestId, string status)
        {
            var request = await _repository.GetByIdAsync<Request>(requestId);

            if (request == null)
            {
                throw new ArgumentException(null, nameof(request));
            }

            request.DeliveryStatus = status;

            if(request.Type != ResourceType.Book.ToString())
            {
                request.DeliveryStatus = ResourceStatus.Delivered.ToString();
            }

            if(request.DeliveryStatus == ResourceStatus.Delivered.ToString())
            {
                var category = await _repository.All<ResourceCategory>(a => a.Name == request.Category).FirstOrDefaultAsync();

                var resourceFromRequest = new Resource
                {
                    Author = request.Author,
                    DateAdded = DateTime.Now,
                    CategoryID = category.Id,
                    Description = request.Description,
                    Link = request.Link,
                    Status = MelonBookchelfApi.Infrastructure.Data.Models.Enums.ResourceStatus.Avalable.ToString(),
                    Title = request.Title,
                    Type = request.Type,
                };

                await _repository.AddAsync(resourceFromRequest);
            }

            _repository.Update(request);
            await _repository.SaveChangesAsync();
        }
    }
}
