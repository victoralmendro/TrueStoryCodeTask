using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrueStoryCodeTask.DTOs;
using TrueStoryCodeTask.DTOs.Filters;
using TrueStoryCodeTask.DTOs.Requests;
using TrueStoryCodeTask.DTOs.Response;
using TrueStoryCodeTask.Errors;
using TrueStoryCodeTask.Services;

namespace TrueStoryCodeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] ProductFilter? filter = null
        )
        {
            if (page <= 0 || pageSize <= 0)
            {
                throw new InvalidRequestParamsException(HttpStatusCode.BadRequest, "Page and pageSize must be positive integers.");
            }

            var all = await _productService.GetAllAsync(page, pageSize, filter);

            var result = new PagedResult<ProductGetResponse>
            {
                Items = all.Items.Select(p => new ProductGetResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Data = new ProductDataGetResponse { 
                        Price = p.Data.Price
                    }
                }).ToList(),
                TotalCount = all.TotalCount,
                Page = all.Page,
                PageSize = all.PageSize
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductPostRequest product)
        {
            var created = await _productService.CreateAsync(new ProductDTO
            {
                Name = product.Name,
                Data = new ProductDataDTO { 
                    Price = product.Data.Price,
                }
            });

            return Ok(created);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(string productId)
        {
            await _productService.DeleteAsync(productId);

            return Ok();
        }
    }
}
