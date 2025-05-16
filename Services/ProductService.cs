using System.Runtime.CompilerServices;
using TrueStoryCodeTask.DTOs;
using TrueStoryCodeTask.DTOs.Filters;
using TrueStoryCodeTask.DTOs.MockApi;
using TrueStoryCodeTask.HttpClients;

namespace TrueStoryCodeTask.Services
{
    public class ProductService
    {
        private readonly MockApiClient _mockApiClient;
        private readonly MockIdStoreService _mockIdStoreService;

        public ProductService(MockApiClient mockApiClient, MockIdStoreService mockIdStoreService)
        {
            _mockApiClient = mockApiClient;
            _mockIdStoreService = mockIdStoreService;
        }

        public async Task DeleteAsync(string productId)
        {
            await _mockApiClient.DeleteAsync(productId);

            _mockIdStoreService.Remove(productId);
        }

        public async Task<ProductDTO> CreateAsync(ProductDTO product)
        {
            var created = await _mockApiClient.CreateAsync(new MockObjectDTO
            {
                Name = product.Name,
                Data = new MockObjectData
                { 
                    ForTrueStory = true,
                    Price = product.Data == null ? 0 : product.Data.Price
                }
            });

            _mockIdStoreService.Add(created.Id);

            ProductDataDTO? dataObj = null;
            if (created.Data != null) {
                dataObj = new ProductDataDTO
                {
                    Price = created.Data.Price
                };
            }

            return new ProductDTO
            {
                Id = created.Id,
                Name = created.Name,
                Data = dataObj,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<PagedResult<ProductDTO>> GetAllAsync(int page, int pageSize, ProductFilter? filter)
        {
            var ids = _mockIdStoreService.GetAll();
            var all = await _mockApiClient.GetAllAsync(ids.ToList());

            var filtered = all;

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
                Data = new ProductDataDTO
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
