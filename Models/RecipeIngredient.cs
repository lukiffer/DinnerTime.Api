using System;
using Newtonsoft.Json;

namespace DinnerTime.Api.Models
{
    public class RecipeIngredient
    {
        [JsonIgnore]
        public int RecipeId { get; set; }

        [JsonIgnore]
        public int IngredientId { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; }
    }
}
