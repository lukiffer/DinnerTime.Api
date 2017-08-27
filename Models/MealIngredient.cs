using System;
using Newtonsoft.Json;

namespace DinnerTime.Api.Models
{
    public class MealIngredient : Ingredient
    {
        [JsonIgnore]
        public Guid MealId { get; set; }

        [JsonIgnore]
        public int IngredientId { get; set; }

        public decimal Quantity { get; set; }
        public string Units { get; set; }
    }
}
