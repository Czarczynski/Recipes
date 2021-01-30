namespace Recipes.Models
{
    public class DTORecipeAddToDbModel
    {
        public int RecipeId { get; set; }
        public int ReadyInMinutes { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ImageType { get; set; }
    }
}