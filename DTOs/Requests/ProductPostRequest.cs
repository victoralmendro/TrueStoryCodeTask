using System.ComponentModel.DataAnnotations;

namespace TrueStoryCodeTask.DTOs.Requests
{
    public class ProductPostRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ProductData Data { get; set; }
    }
    public class ProductData
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
