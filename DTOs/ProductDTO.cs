using System.Text.Json.Serialization;

namespace TrueStoryCodeTask.DTOs
{
    public class ProductDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public int Name { get; set; }
        
        [JsonPropertyName("data")]
        public ProductData Data { get; set; }
    }
    public class ProductData
    {
        [JsonPropertyName("color")]
        public string Color { get; set; }
        
        [JsonPropertyName("capacity")]
        public string Capacity { get; set; }
    }
}
