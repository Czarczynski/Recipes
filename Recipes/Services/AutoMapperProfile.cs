using AutoMapper;
using Recipes.Models;

namespace Recipes.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User
            CreateMap<Users,DTOUserModel>();
            
            //Favourites
            CreateMap<UserFavouriteRecipes,DTOShortDbRecipeModel>();
            
            CreateMap<DTORecipeAddToDbModel,UserFavouriteRecipes>();
            CreateMap<UserFavouriteRecipes,DTORecipeAddToDbModel>(); // reversed
            
            //LastSeen
            CreateMap<UserLastSeenRecipes,DTOShortDbRecipeModel>();
            
            CreateMap<DTORecipeAddToDbModel,UserLastSeenRecipes>();
            CreateMap<UserLastSeenRecipes,DTORecipeAddToDbModel>(); // reversed
        }
        
    }
}