using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.ResponceModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using IResourceService = MelonBookshelfApi.Services.Contracts.IResourceService;

namespace MelonBookshelfApi.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public ResourceService(IRepository repository, ILogger<ResourceService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchResult>> SearchResources(string? type, string? category, string? title)
        {
            var allResources = _repository
                .All<Resource>()
                .Include(a=>a.ResourceCategory)
                .AsQueryable();

            if (!string.IsNullOrEmpty(type))
            {
                allResources = allResources.Where(r => r.Type.ToString() == type);
            }
            if (!string.IsNullOrEmpty(category))
            {
                allResources = allResources.Where(r => r.ResourceCategory.Name == category);
            }
            if (!string.IsNullOrEmpty(title))
            {
                allResources = allResources.Where(r => r.Title == title);
            }

            var serachResultCollection = new List<SearchResult>();

            foreach (var item in allResources)
            {
                var itemCategory = await _repository.GetByIdAsync<ResourceCategory>(item.CategoryID);

                serachResultCollection.Add(new SearchResult
                {
                    Type = item.Type,
                    Title = item.Title,
                    Author = item.Author,
                    Description = item.Description,
                    Link = item.Link,
                    Status = item.Status,
                    DateAdded = item.DateAdded,
                    PurchasePrice = item.PurchasePrice,
                    InvoiceAttachment = item.InvoiceAttachment,
                    ResourceDetails = item.ResourceDetails,
                    ResourceCategory = itemCategory
                });
            }

            return serachResultCollection;
        }
    }
}
