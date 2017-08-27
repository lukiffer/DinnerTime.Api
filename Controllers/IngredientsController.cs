using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DinnerTime.Api.Models;
using DinnerTime.Api.Repositories;

namespace DinnerTime.Api.Controllers
{
    [Route("ingredients")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientRepository _repository;

        public IngredientsController()
        {
            _repository = new IngredientRepository();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _repository.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Ingredient ingredient)
        {
            var ingredientId = await _repository.Create(ingredient);
            return new OkObjectResult(await _repository.Get(ingredientId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Ingredient ingredient)
        {
            if (id != ingredient.Id) {
                return new BadRequestResult();
            }

            await _repository.Update(ingredient);
            return new OkObjectResult(await _repository.Get(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return new OkObjectResult(new { Success = true });
        }

		[HttpGet("categories")]
		public async Task<IActionResult> GetCategories()
		{
            return new OkObjectResult(await _repository.GetCategories());
		}
    }
}
