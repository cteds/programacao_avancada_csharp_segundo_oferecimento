using deploy_sample.Interfaces;
using deploy_sample.Models;
using deploy_sample.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace deploy_sample.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProduct _product { get; set; }

        public ProductsController()
        {
            _product = new ProductRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_product.ReadAll()); // ok
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult Post(Product newProduct)
        {
            try
            {
                _product.Create(newProduct);

                return StatusCode(201); // created
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
