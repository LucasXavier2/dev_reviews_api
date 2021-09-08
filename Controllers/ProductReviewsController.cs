using System.Threading.Tasks;
using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/[controller]")]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductReviewsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var productReview = await _repository.GetReviewByIdAsync(id);

            if (productReview == null) 
            {
                return NotFound();
            }

            var productReviewDetails = _mapper.Map<ProductReviewDetailsViewModel>(productReview);

            return Ok(productReviewDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int productId, AddProductReviewInputModel model) 
        {
            var productReview = new ProductReview(model.Author, model.Rating, model.Comments, productId);

            await _repository.AddReviewAsync(productReview);

            return CreatedAtAction(nameof(GetById), new { id = productReview.Id, productId = productId}, model);
        }
    }
}