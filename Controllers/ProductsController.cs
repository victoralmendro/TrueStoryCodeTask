using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrueStoryCodeTask.DTOs.Filters;
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

            return Ok(
                await _productService.GetAllAsync(page, pageSize, filter)
            );
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
