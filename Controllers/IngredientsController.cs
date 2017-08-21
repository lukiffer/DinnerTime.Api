using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DinnerTime.Api.Models;
using DinnerTime.Api.Repositories;

namespace DinnerTime.Api.Controllers
{
    [Route("ingredients")]
    public class IngredientsController : Controller
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
            var id = await _repository.Create(ingredient);
            return new OkObjectResult(_repository.Get(id));
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
            return new OkResult();
        }
    }
}
