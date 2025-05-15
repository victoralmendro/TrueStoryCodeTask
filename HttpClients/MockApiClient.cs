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
        public async Task<IEnumerable<MockObjectDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("objects");

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("GetAllSync | Status: {Status}. Message: {Message}", response.StatusCode, message);
                
                throw new IntegrationException(response.StatusCode, "Failed to fetch objects from external API.");
            }

            return await response.Content.ReadFromJsonAsync<List<MockObjectDTO>>() ?? new();
        }
    }
}
