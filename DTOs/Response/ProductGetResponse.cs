namespace TrueStoryCodeTask.DTOs.Response
{
    public class ProductGetResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ProductDataGetResponse Data { get; set; }
    }

    public class ProductDataGetResponse
    {
        public decimal Price { get; set; }
    }
}
