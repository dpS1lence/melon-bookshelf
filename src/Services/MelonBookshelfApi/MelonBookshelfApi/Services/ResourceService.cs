using AutoMapper;
using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.ResponceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using static System.Reflection.Metadata.BlobBuilder;
using IResourceService = MelonBookshelfApi.Services.Contracts.IResourceService;

namespace MelonBookshelfApi.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public ResourceService(IRepository repository, ILogger<ResourceService> logger, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IEnumerable<string> GetCategoriesAsync()
        {
            var categories = _repository.All<ResourceCategory>().Select(a => a.Name).AsEnumerable();

            return categories;
        }

        public async Task<PhysicalResource> GetPhysicalResourceByIdAsync(string pysicalResourceId)
        {
            var resource = await _repository.GetByIdAsync<Resource>(pysicalResourceId);

            var map = _mapper.Map<PhysicalResource>(resource);

            return map;
        }

        public async Task<IEnumerable<PhysicalResourceTaken>> GetPhysicalResourcesByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null) 
            {
                throw new ArgumentException("User is null");
            }

            var userResources = await _repository
                .All<UserResource>()
                .Where(a => a.UserId == userId)
                .Include(a => a.Resource)
                .Select(a => a.Resource)
                .ToListAsync();

            var map = _mapper.Map<IEnumerable<PhysicalResourceTaken>>(userResources);

            return map;
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
