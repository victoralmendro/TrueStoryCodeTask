namespace TrueStoryCodeTask.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public ProductDataDTO Data { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
    public class ProductDataDTO
    {
        public decimal Price { get; set; }
    }
}
