using System;

namespace Recipes.Models
{
    public class DTOShortDbRecipeModel
    {
        public int RecipeId { get; set; }
        public DateTime AddedDate { get; set; }
        public int ReadyInMinutes { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ImageType { get; set; }
    }
}