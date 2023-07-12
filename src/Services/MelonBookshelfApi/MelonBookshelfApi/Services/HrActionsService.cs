using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookchelfApi.Infrastructure.Data.Models.Enums;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.Services.Contracts;

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

            _repository.Update(request);
            await _repository.SaveChangesAsync();
        }
    }
}
