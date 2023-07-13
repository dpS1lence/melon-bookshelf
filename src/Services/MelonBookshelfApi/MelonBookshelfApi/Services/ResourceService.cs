using AutoMapper;
using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.ResponceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddResource(ResourceModel model)
        {
            var resource = _mapper.Map<Resource>(model);

            var category = await _repository.All<ResourceCategory>().Where(a => a.Name == model.ResourceCategory.Name).FirstOrDefaultAsync();

            if(category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            resource.CategoryID = category.Id;

            await _repository.AddAsync(resource);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResourceModel>> GetAllResources()
        {
            var resources = await _repository.All<Resource>().AsNoTracking().ToListAsync();

            var list = new List<ResourceModel>();

            foreach (var item in resources)
            {
                var result = await _repository
                    .All<ResourceCategory>()
                    .AsNoTracking()
                    .Where(a => a.Id == item.CategoryID)
                    .FirstOrDefaultAsync();

                item.ResourceCategory = result;

                list.Add(_mapper.Map<ResourceModel>(item));
            }

            return list;
        }

        public async Task<IEnumerable<ResourceModel>> GetAllResourcesHR()
        {
            var resources = await _repository.All<Resource>().AsNoTracking().ToListAsync();

            var list = new List<ResourceModel>();

            foreach (var item in resources)
            {
                var result = await _repository
                    .All<ResourceCategory>()
                    .AsNoTracking()
                    .Where(a => a.Id == item.CategoryID)
                    .FirstOrDefaultAsync();

                item.ResourceCategory = result;

                list.Add(_mapper.Map<ResourceModel>(item));
            }

            return list;
        }

        public IEnumerable<string> GetCategoriesAsync()
        {
            var categories = _repository.All<ResourceCategory>().Select(a => a.Name).AsEnumerable();

            return categories;
        }

        public async Task<ResourceModel> GetPhysicalResourceByIdAsync(string pysicalResourceId)
        {
            var resource = await _repository.GetByIdAsync<Resource>(pysicalResourceId);

            var map = _mapper.Map<ResourceModel>(resource);

            return map;
        }

        public async Task<IEnumerable<ResourceModel>> GetPhysicalResourcesByUserIdAsync(string userId)
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

            var map = _mapper.Map<IEnumerable<ResourceModel>>(userResources);

            return map;
        }

        public async Task<ResourceModel> GetResourceByIdAsync(string resourceId)
        {
            var resource = await _repository.GetByIdAsync<Resource>(resourceId);

            if (resource == null)
            {
                throw new ArgumentException("Resource is null");
            }

            var map = _mapper.Map<ResourceModel>(resource);

            return map;
        }

        public async Task<IEnumerable<ResourceModel>> SearchResources(string? type, string? category, string? title)
        {
            var allResources = await _repository
                .All<Resource>()
                .AsNoTracking()
                .Include(a=>a.ResourceCategory)
                .ToListAsync();

            if (!string.IsNullOrEmpty(type))
            {
                allResources = allResources.Where(r => r.Type.ToString() == type).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                allResources = allResources.Where(r => r.ResourceCategory.Name == category).ToList();
            }
            if (!string.IsNullOrEmpty(title))
            {
                allResources = allResources.Where(r => r.Title == title).ToList();
            }

            var serachResultCollection = new List<ResourceModel>();

            foreach (var item in allResources)
            {
                var itemCategory = await _repository.GetByIdAsync<ResourceCategory>(item.CategoryID);

                serachResultCollection.Add(new ResourceModel
                {
                    Id = item.Id,
                    Type = item.Type.ToString(),
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
