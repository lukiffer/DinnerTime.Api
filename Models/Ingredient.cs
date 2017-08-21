using System.ComponentModel.DataAnnotations;

namespace DinnerTime.Api.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public decimal? DensityMultiplier { get; set; }
        public NutritionFactList NutritionFacts { get; set; }
    }
}
