using AutoMapper;
using Recipes.Models;

namespace Recipes.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users,DTOUserModel>();
        }
        
    }
}