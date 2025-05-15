using TrueStoryCodeTask.DTOs;
using TrueStoryCodeTask.DTOs.Filters;
using TrueStoryCodeTask.HttpClients;

namespace TrueStoryCodeTask.Services
{
    public class ProductService
    {
        private readonly MockApiClient _mockApiClient;

        public ProductService(MockApiClient mockApiClient)
        {
            _mockApiClient = mockApiClient;
        }

        public async Task<PagedResult<ProductDTO>> GetAllAsync(int page, int pageSize, ProductFilter? filter)
        {
            var all = await _mockApiClient.GetAllAsync();

            var filtered = all.Where(p => p.Data?.ForTrueStory == true);

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    filtered = filtered.Where(p => p.Name.Contains(filter.Name));
                }
            }

            var products = filtered.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Data = p.Data == null ? null : new ProductData
                {
                    Price = p.Data.Price
                }
            });

            var paged = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<ProductDTO>
            {
                Items = paged,
                TotalCount = filtered.Count(),
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
