using System;
using Newtonsoft.Json;

namespace DinnerTime.Api.Models
{
    public class MealRecipe : Recipe
    {
        [JsonIgnore]
        public Guid MealId { get; set; }

        [JsonIgnore]
        public int RecipeId { get; set; }

        [JsonIgnore]
        public override int Id { get; set; }

        public MealPhase MealPhase { get; set; }
        public decimal Quantity { get; set; }
    }
}
