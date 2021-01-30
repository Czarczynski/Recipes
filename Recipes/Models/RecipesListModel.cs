using System.Collections.Generic;
using Newtonsoft.Json;

namespace Recipes.Models
{
    public class RecipesListModel
    {
        [JsonProperty("results")] public List<ShortRecipeModel> Results { get; set; }
    }

    public class ShortRecipeModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("image")] public string Image { get; set; }
        [JsonProperty("imageType")] public string ImageType { get; set; }
    }
}