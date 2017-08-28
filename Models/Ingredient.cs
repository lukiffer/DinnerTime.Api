using System.ComponentModel.DataAnnotations;

namespace DinnerTime.Api.Models
{
    public class IngredientBase
    {
		[Key]
		public virtual int Id { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
        public bool IsCertified { get; set; }
		public decimal Calories { get; set; }
		public decimal TotalFat { get; set; }
        public decimal TotalCarbohydrate { get; set; }
        public decimal Protein { get; set; }
    }

    public class Ingredient : IngredientBase
    {
        
        public int? ExternalId { get; set; }

        public string ImageUrl { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public decimal DensityMultiplier { get; set; }
		public decimal SaturatedFat { get; set; }
		public decimal TransFat { get; set; }
		public decimal PolyunsaturatedFat { get; set; }
		public decimal MonounsaturatedFat { get; set; }
		public decimal Sugar { get; set; }
		public decimal Fiber { get; set; }
		public decimal Cholesterol { get; set; }
		public decimal Sodium { get; set; }
    }
}