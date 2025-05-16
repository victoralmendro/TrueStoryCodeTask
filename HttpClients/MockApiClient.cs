using System.Net;
using TrueStoryCodeTask.DTOs.MockApi;
using TrueStoryCodeTask.Errors;

namespace TrueStoryCodeTask.HttpClients
{
    public class MockApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MockApiClient> _logger;
        public MockApiClient(HttpClient httpClient, ILogger<MockApiClient> logger) {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IEnumerable<MockObjectDTO>> GetAllAsync(List<string> ids)
        {
            if(ids.Count == 0)
            {
                return new List<MockObjectDTO>();
            }

            string queryParams = string.Join("&", ids.Select(id => $"id={Uri.EscapeDataString(id)}"));
            var response = await _httpClient.GetAsync($"objects?{queryParams}");

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("GetAllSync | Status: {Status}. Message: {Message}", response.StatusCode, message);
                
                throw new IntegrationException(response.StatusCode, "Failed to fetch objects from external API.");
            }

            return await response.Content.ReadFromJsonAsync<List<MockObjectDTO>>() ?? new();
        }

        public async Task<MockObjectDTO> CreateAsync(MockObjectDTO obj)
        {
            var response = await _httpClient.PostAsJsonAsync("objects", obj);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("CreateAsync | Status: {Status}. Message: {Message}", response.StatusCode, message);

                throw new IntegrationException(response.StatusCode, "Failed to create object on external API.");
            }

            var createdObject = await response.Content.ReadFromJsonAsync<MockObjectDTO>();

            if (createdObject == null)
            {
                _logger.LogError("CreateAsync | Deserialized object is null.");
                throw new IntegrationException(HttpStatusCode.InternalServerError, "Invalid response from external API.");
            }

            return createdObject;
        }

        public async Task<DeleteMockObjectDTO> DeleteAsync(string objectId)
        {
            var response = await _httpClient.DeleteAsync($"objects/{objectId}");

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("CreateAsync | Status: {Status}. Message: {Message}", response.StatusCode, message);

                throw new IntegrationException(response.StatusCode, "Failed to delete object on external API.");
            }

            var responseObject = await response.Content.ReadFromJsonAsync<DeleteMockObjectDTO>();

            if (responseObject == null)
            {
                _logger.LogError("DeleteAsync | Deserialized object is null.");
                throw new IntegrationException(HttpStatusCode.InternalServerError, "Invalid response from external API.");
            }

            return responseObject;
        }
    }
}
