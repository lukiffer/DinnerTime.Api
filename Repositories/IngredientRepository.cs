using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DinnerTime.Api.Models;

namespace DinnerTime.Api.Repositories
{
    public class IngredientRepository : RepositoryBase
    {
        public async Task<IEnumerable<Ingredient>> GetAll()
        {
            using (var connection = await ConnectAsync())
            {
                return await connection.QueryAsync<Ingredient>("SELECT * FROM Ingredients");
            }
        }

        public async Task<Ingredient> Get(int id)
        {
            using (var connection = await ConnectAsync())
            {
                return (await connection.QueryAsync<Ingredient>(
                    "SELECT * FROM Ingredients WHERE Id=@Id", new { Id = id }))
                        .SingleOrDefault();
            }
        }

        public async Task<int> Create(Ingredient ingredient)
        {
            using (var connection = await ConnectAsync())
            {
                return await connection.ExecuteScalarAsync<int>(@"
                    INSERT INTO Ingredients (Name, Category, ImageUrl, MeasurementType, DensityMultiplier)
                    VALUES(@Name, @Category, @ImageUrl, @MeasurementType, @DensityMultiplier)
                    SELECT CAST(SCOPE_IDENTITY() AS INT)", ingredient);
            }
        }

        public async Task Update(Ingredient ingredient)
        {
            using (var connection = await ConnectAsync())
            {
                await connection.ExecuteAsync(@"UPDATE Ingredients
                    SET 
                        Name=@Name,
                        Category=@Category,
                        ImageUrl=@ImageUrl,
                        MeasurementType=@MeasurementType,
                        DensityMultiplier=@DensityMultiplier
                    WHERE
                        Id=@Id", ingredient); 
            }
		}

        public async Task Delete(int id)
        {
            using (var connection = await ConnectAsync())
            {
                await connection.ExecuteAsync("DELETE FROM Ingredients WHERE Id=@Id", new { Id = id });
            }
        }
	}
 }