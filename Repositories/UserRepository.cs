using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DinnerTime.Api.Models;

namespace DinnerTime.Api.Repositories
{
	public class UserRepository : RepositoryBase
	{
		public async Task<IEnumerable<User>> GetAll()
		{
			using (var connection = await ConnectAsync())
			{
				return await connection.QueryAsync<User>(
					"SELECT * FROM Users");
			}
		}

		public async Task<User> Get(string id)
		{
			using (var connection = await ConnectAsync())
			{
				return (await connection.QueryAsync<User>(
					"SELECT * FROM Users WHERE Id=@Id",
					new { Id = id })).SingleOrDefault();
			}
		}

		public async Task<string> Create(User user)
		{
			using (var connection = await ConnectAsync())
			{
				await connection.ExecuteAsync(
                    "INSERT INTO Users (Id, Name) VALUES (@Id, @Name)", user);

                return user.Id;
			}
		}

		public async Task Update(User user)
		{
			using (var connection = await ConnectAsync())
			{
				await connection.ExecuteAsync(
                    "UPDATE Users SET Name = @Name WHERE Id = @Id", user);
			}
		}
	}
}