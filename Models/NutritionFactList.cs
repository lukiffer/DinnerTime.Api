using System;
using System.ComponentModel.DataAnnotations;

namespace DinnerTime.Api.Models
{
    public class NutritionFactList
    {
        [Key]
        public Guid Id { get; set; }
        public int ServingSize { get; set; }
        public decimal Calories { get; set; }
        public decimal TotalFat { get; set; }
        public decimal SaturatedFat { get; set; }
        public decimal TransFat { get; set; }
        public decimal PolyunsaturatedFat { get; set; }
        public decimal MonounsaturatedFat { get; set; }
        public decimal TotalCarbohydrate { get; set; }
        public decimal Sugar { get; set; }
        public decimal Fiber { get; set; }

        public decimal NetCarbs
        {
            get
            {
                return TotalCarbohydrate - Fiber;
            }
        }

        public decimal Protein { get; set; }
        public decimal Cholesterol { get; set; }
        public decimal Sodium { get; set; }
    }
}
