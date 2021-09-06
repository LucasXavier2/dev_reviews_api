using DevReviews.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.Controllers 
{
    [ApiController]
    [Route("api/products/{productId}/[controller]")]
    public class ProductReviewsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            //se não existir id especificado, retornar notfound.
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(int productId, AddProductReviewInputModel model) 
        {
            //se tiver erros de validação, retornar badrequest
            return CreatedAtAction(nameof(GetById), new { id = 1, productId = 2}, model);
        }
    }
}