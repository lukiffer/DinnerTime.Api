using System;
using Newtonsoft.Json;

namespace DinnerTime.Api.Models
{
    public class RecipeIngredient : Ingredient
    {
        public int RecipeId { get; set; }

        public int IngredientId { get; set; }

        public decimal Quantity { get; set; }

        public string Units { get; set; }
    }
}
