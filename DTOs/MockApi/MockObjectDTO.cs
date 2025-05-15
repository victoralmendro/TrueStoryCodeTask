using System.Text.Json.Serialization;

namespace TrueStoryCodeTask.DTOs.MockApi
{
    public class MockObjectDTO
    {
        [JsonPropertyName("id")]
        public int Id{ get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("data")]
        public MockObjectData? Data { get; set; }
    }

    public class MockObjectData
    {
        [JsonPropertyName("forTrueStory")]
        public bool ForTrueStory { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
