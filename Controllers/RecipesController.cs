using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DinnerTime.Api.Models;
using DinnerTime.Api.Repositories;

namespace DinnerTime.Api.Controllers
{
	[Route("recipes")]
	public class RecipesController : ControllerBase
	{
		private readonly RecipeRepository _repository;

		public RecipesController()
		{
			_repository = new RecipeRepository();
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
            return new OkObjectResult(await _repository.GetAll());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
            var result = await _repository.Get(id);
            result.Ingredients = (await _repository.GetRecipeIngredients(result.Id)).ToList();
            return new OkObjectResult(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]Recipe recipe)
		{
            recipe.Ingredients.ForEach(x => x.RecipeId = recipe.Id);
            recipe.LastUpdated = DateTime.UtcNow;

            var recipeId = await _repository.Create(recipe);
            foreach (var recipeIngredient in recipe.Ingredients)
            {
                await _repository.CreateRecipeIngredient(recipeIngredient);
            }

            return new OkObjectResult(await _repository.Get(recipeId));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]Recipe recipe)
		{
            if (recipe.Id != id)
            {
                return new BadRequestResult();
            }

            recipe.Ingredients.ForEach(x => x.RecipeId = recipe.Id);
            recipe.LastUpdated = DateTime.UtcNow;

            await _repository.Update(recipe);

            var recipeIngredients = (await _repository.GetRecipeIngredients(recipe.Id)).ToList();

            recipe.Ingredients.ForEach(async x =>
            {
                var recipeIngredient = recipeIngredients.SingleOrDefault(y => y.IngredientId == x.IngredientId);
                if (recipeIngredient == null)
                {
                    await _repository.CreateRecipeIngredient(x);
                }
                else
                {
                    await _repository.UpdateRecipeIngredient(x);
                }
            });

            recipeIngredients.ForEach(async x =>
            {
                var recipeIngredient = recipe.Ingredients.SingleOrDefault(y => y.IngredientId == x.IngredientId);
                if (recipeIngredient == null)
                {
                    await _repository.DeleteRecipeIngredient(x);
                }
            });

            return new OkObjectResult(recipe);
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
