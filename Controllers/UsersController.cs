using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DinnerTime.Api.Models;
using DinnerTime.Api.Repositories;

namespace DinnerTime.Api.Controllers
{
	[Route("users")]
	public class UsersController : ControllerBase
	{
		readonly UserRepository _repository;

		public UsersController()
		{
			_repository = new UserRepository();
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return new OkObjectResult(await _repository.GetAll());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			return new OkObjectResult(await _repository.Get(id));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(string id, [FromBody]User user)
		{
            var userId = GetUserId();
            if (userId != id || userId != user.Id)
            {
                return new ForbidResult();
            }

            var existing = await _repository.Get(id);

            if (existing != null)
            {
                await _repository.Update(user);
            }
            else
            {
                await _repository.Create(user);
            }
			
			return new OkObjectResult(await _repository.Get(id));
		}
	}
}
