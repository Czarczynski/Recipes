using System;

namespace Recipes.Common
{
    public static class Consts
    {
        public static readonly string SECRET = Environment.GetEnvironmentVariable("SecretKey");
        public static readonly string SPOONACULAR_API_KEY = Environment.GetEnvironmentVariable("spoonacularApiKey");
    }
}