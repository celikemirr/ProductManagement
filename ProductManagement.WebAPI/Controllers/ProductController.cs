using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Entities.Models;
using ProductManagement.WebUI.Helpers;

namespace ProductManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductManagementContext context = new ProductManagementContext();

        private readonly IWebHostEnvironment _environment;

        public ProductController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get()
        {
            var product = context.Products.Where(x => x.Status == true).ToList();
            if (product.Count() == 0)
            {
                return NoContent();
            }
            return Ok(product);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get(int id)
        {
            var products = context.Products.FirstOrDefault(x => x.Status == true && x.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("DeactivatedUsers")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeactivatedUsers()
        {
            var product = context.Products.Where(x => x.Status == false).ToList();
            if (product.Count() == 0)
            {
                return NoContent();
            }
            return Ok(product);
        }


        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Post(Product model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.ImageURL = await UploadHelper.UploadImageAsync(_environment, img);
                }
                model.Status = true;
                model.CreatedDate = DateTime.Now;
                context.Products.Add(model);
                context.SaveChanges();
                return Ok(model);
            }
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Put(Product model)
        {
            var product = context.Products.FirstOrDefault(x => x.Status == true && x.Id == model.Id);

            if (product == null)
            {
                return NotFound();
            }
            product.Price = model.Price;
            product.Name = model.Name;
            context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Delete(int id)
        {
            var product = context.Users.FirstOrDefault(x => x.Status == true && x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Status = false;
            context.SaveChanges();
            return Ok(product);
        }
    }
}
