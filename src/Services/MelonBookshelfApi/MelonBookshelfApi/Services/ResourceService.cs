using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.ResponceModels;
using System.ComponentModel.Design;
using IResourceService = MelonBookshelfApi.Services.Contracts.IResourceService;

namespace MelonBookshelfApi.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public ResourceService(IRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchResult>> SearchResources(string type, string category, string title)
        {
            throw new NotImplementedException();
        }
    }
}
