using TrueStoryCodeTask.DTOs;

namespace TrueStoryCodeTask.Services
{
    public class ProductService
    {
        private readonly string _endpoint = "https://api.restful-api.dev/objects";
        public async Task<List<ProductDTO>> getAllAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<ProductDTO>>(_endpoint);

            if (response == null)
            {
                return new List<ProductDTO>();
            }

            return response;
        }
    }
}
