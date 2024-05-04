using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using ApiConsumeProducts.Model;

namespace ApiConsumeProducts.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProductsController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessedProducts()
        {
            //var response = await _httpClient.GetAsync("http://localhost:5000/api/products");
            var response = await _httpClient.GetAsync("https://localhost:7179/ServicoPythonProducts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(content);

                // Process the data (example: filter by price)
                var processedProducts = products.Where(p => p.price > 10).ToList();

                return Ok(processedProducts);
            }

            return NotFound();
        }
    }
}