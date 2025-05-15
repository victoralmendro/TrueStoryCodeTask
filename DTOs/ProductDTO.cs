namespace TrueStoryCodeTask.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public ProductData? Data { get; set; }
    }
    public class ProductData
    {
        public decimal Price { get; set; }
    }
}
