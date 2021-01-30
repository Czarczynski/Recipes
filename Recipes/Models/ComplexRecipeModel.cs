using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Recipes.Models
{
    public class ComplexRecipeModel
    {
        [JsonProperty("vegetarian", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Vegetarian { get; set; }

        [JsonProperty("vegan", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Vegan { get; set; }

        [JsonProperty("glutenFree", NullValueHandling = NullValueHandling.Ignore)]
        public bool? GlutenFree { get; set; }

        [JsonProperty("dairyFree", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DairyFree { get; set; }

        [JsonProperty("veryHealthy", NullValueHandling = NullValueHandling.Ignore)]
        public bool? VeryHealthy { get; set; }

        [JsonProperty("cheap", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Cheap { get; set; }

        [JsonProperty("veryPopular", NullValueHandling = NullValueHandling.Ignore)]
        public bool? VeryPopular { get; set; }

        [JsonProperty("sustainable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Sustainable { get; set; }

        [JsonProperty("weightWatcherSmartPoints", NullValueHandling = NullValueHandling.Ignore)]
        public long? WeightWatcherSmartPoints { get; set; }

        [JsonProperty("gaps", NullValueHandling = NullValueHandling.Ignore)]
        public string Gaps { get; set; }

        [JsonProperty("lowFodmap", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LowFodmap { get; set; }

        [JsonProperty("preparationMinutes", NullValueHandling = NullValueHandling.Ignore)]
        public long? PreparationMinutes { get; set; }

        [JsonProperty("cookingMinutes", NullValueHandling = NullValueHandling.Ignore)]
        public long? CookingMinutes { get; set; }

        [JsonProperty("aggregateLikes", NullValueHandling = NullValueHandling.Ignore)]
        public long? AggregateLikes { get; set; }

        [JsonProperty("spoonacularScore", NullValueHandling = NullValueHandling.Ignore)]
        public long? SpoonacularScore { get; set; }

        [JsonProperty("healthScore", NullValueHandling = NullValueHandling.Ignore)]
        public long? HealthScore { get; set; }

        [JsonProperty("creditsText", NullValueHandling = NullValueHandling.Ignore)]
        public string CreditsText { get; set; }

        [JsonProperty("sourceName", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceName { get; set; }

        [JsonProperty("pricePerServing", NullValueHandling = NullValueHandling.Ignore)]
        public double? PricePerServing { get; set; }

        [JsonProperty("extendedIngredients", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExtendedIngredient> ExtendedIngredients { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("readyInMinutes", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReadyInMinutes { get; set; }

        [JsonProperty("servings", NullValueHandling = NullValueHandling.Ignore)]
        public long? Servings { get; set; }

        [JsonProperty("sourceUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SourceUrl { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Image { get; set; }

        [JsonProperty("imageType", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageType { get; set; }

        [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
        public string Summary { get; set; }

        [JsonProperty("cuisines", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Cuisines { get; set; }

        [JsonProperty("dishTypes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> DishTypes { get; set; }

        [JsonProperty("diets", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Diets { get; set; }

        [JsonProperty("occasions", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Occasions { get; set; }

        [JsonProperty("instructions", NullValueHandling = NullValueHandling.Ignore)]
        public string Instructions { get; set; }

        [JsonProperty("analyzedInstructions", NullValueHandling = NullValueHandling.Ignore)]
        public List<AnalyzedInstruction> AnalyzedInstructions { get; set; }

        [JsonProperty("originalId")] public object OriginalId { get; set; }
    }

    public class AnalyzedInstruction
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("steps", NullValueHandling = NullValueHandling.Ignore)]
        public List<Step> Steps { get; set; }
    }

    public class Step
    {
        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public long? Number { get; set; }

        [JsonProperty("step", NullValueHandling = NullValueHandling.Ignore)]
        public string StepStep { get; set; }

        [JsonProperty("ingredients", NullValueHandling = NullValueHandling.Ignore)]
        public List<Ent> Ingredients { get; set; }

        [JsonProperty("equipment", NullValueHandling = NullValueHandling.Ignore)]
        public List<Ent> Equipment { get; set; }

        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public Length Length { get; set; }
    }

    public class Ent
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("localizedName", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalizedName { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }
    }

    public class Length
    {
        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public long? Number { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }
    }

    public class ExtendedIngredient
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("aisle", NullValueHandling = NullValueHandling.Ignore)]
        public string Aisle { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }

        [JsonProperty("consistency", NullValueHandling = NullValueHandling.Ignore)]
        public Consistency? Consistency { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("original", NullValueHandling = NullValueHandling.Ignore)]
        public string Original { get; set; }

        [JsonProperty("originalString", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalString { get; set; }

        [JsonProperty("originalName", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalName { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }

        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Meta { get; set; }

        [JsonProperty("metaInformation", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MetaInformation { get; set; }

        [JsonProperty("measures", NullValueHandling = NullValueHandling.Ignore)]
        public Measures Measures { get; set; }
    }

    public class Measures
    {
        [JsonProperty("us", NullValueHandling = NullValueHandling.Ignore)]
        public Metric Us { get; set; }

        [JsonProperty("metric", NullValueHandling = NullValueHandling.Ignore)]
        public Metric Metric { get; set; }
    }

    public class Metric
    {
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }

        [JsonProperty("unitShort", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitShort { get; set; }

        [JsonProperty("unitLong", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitLong { get; set; }
    }

    public enum Consistency
    {
        Liquid,
        Solid
    };
}