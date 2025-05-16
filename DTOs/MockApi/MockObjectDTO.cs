using System.Text.Json.Serialization;

namespace TrueStoryCodeTask.DTOs.MockApi
{
    public class MockObjectDTO
    {
        [JsonPropertyName("id")]
        public string Id{ get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("data")]
        public MockObjectData Data { get; set; }
        
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }
    }

    public class MockObjectData
    {
        [JsonPropertyName("forTrueStory")]
        public bool ForTrueStory { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
