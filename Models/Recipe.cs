using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DinnerTime.Api.Models
{
    public class RecipeBase
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string Instructions { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Servings { get; set; }
		public decimal Calories { get; set; }
		public decimal TotalFat { get; set; }
		public decimal TotalCarbohydrate { get; set; }
		public decimal Protein { get; set; }
    }

    public class Recipe : RecipeBase
    {
        public List<RecipeIngredient> Ingredients { get; set; }
    }
}
