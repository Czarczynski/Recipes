using System;

namespace Recipes.Services
{
    public class UserFavouriteRecipes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public DateTime AddedDate { get; set; }
        public int ReadyInMinutes { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ImageType { get; set; }
    }
}