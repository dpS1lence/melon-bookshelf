using AutoMapper;
using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookshelfApi.RequestDtos;
using MelonBookshelfApi.ResponceModels;

namespace MelonBookshelfApi.MapperProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Resource, PhysicalResource>().ReverseMap();
            CreateMap<Resource, PhysicalResourceTaken>().ReverseMap();
            CreateMap<Request, ResourceRequestDto>().ReverseMap();
            CreateMap<Resource, ResourceModel>().ReverseMap();
        }
    }
}
