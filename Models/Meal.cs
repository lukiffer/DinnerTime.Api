using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DinnerTime.Api.Models
{
    public class MealBase
    {
        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
        public MealType Type { get; set; }
        public DateTime Date { get; set; }
    }

    public class Meal : MealBase
    {
        public List<MealRecipe> Recipes { get; set; }
        public List<MealIngredient> Ingredients { get; set; }
    }
}
