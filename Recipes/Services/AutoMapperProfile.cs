using AutoMapper;
using Recipes.Models;

namespace Recipes.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users,DTOUserModel>();
            
            CreateMap<UserFavouriteRecipes,DTOShortDbRecipeModel>();
            
            CreateMap<DTORecipeAddToDbModel,UserFavouriteRecipes>();
            CreateMap<UserFavouriteRecipes,DTORecipeAddToDbModel>(); // reversed
            
            CreateMap<UserLastSeenRecipes,DTOShortDbRecipeModel>();
            
            CreateMap<DTOShortDbRecipeModel,DTORecipeAddToDbModel>(); //reversed
        }
        
    }
}