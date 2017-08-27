using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DinnerTime.Api.Models;

namespace DinnerTime.Api.Repositories
{
    public class IngredientRepository : RepositoryBase
    {
        public async Task<IEnumerable<IngredientBase>> GetAll()
        {
            using (var connection = await ConnectAsync())
            {
                return await connection.QueryAsync<IngredientBase>(
                    "SELECT * FROM Ingredients");
            }
        }

        public async Task<Ingredient> Get(int id)
        {
            using (var connection = await ConnectAsync())
            {
				return (await connection.QueryAsync<Ingredient>(
                    "SELECT * FROM Ingredients WHERE Id=@Id",
                    new { Id = id })).SingleOrDefault();
            }
        }

        public async Task<int> Create(Ingredient ingredient)
        {
            using (var connection = await ConnectAsync())
            {
                return await connection.ExecuteScalarAsync<int>(@"
                    INSERT INTO Ingredients (
                        ExternalId,
                        [Name],
                        Category,
                        ImageUrl,
                        MeasurementType,
                        DensityMultiplier,
                        IsCertified,
                        Calories,
                        TotalFat,
                        SaturatedFat,
                        TransFat,
                        PolyunsaturatedFat,
                        MonounsaturatedFat,
                        TotalCarbohydrate,
                        Sugar,
                        Fiber,
                        Protein,
                        Cholesterol,
                        Sodium
                    )
                    VALUES (
                        @ExternalId,
                        @Name,
                        @Category,
                        @ImageUrl,
                        @MeasurementType,
                        @DensityMultiplier,
                        @IsCertified,
                        @Calories,
                        @TotalFat,
                        @SaturatedFat,
                        @TransFat,
                        @PolyunsaturatedFat,
                        @MonounsaturatedFat,
                        @TotalCarbohydrate,
                        @Sugar,
                        @Fiber,
                        @Protein,
                        @Cholesterol,
                        @Sodium
                    )
                    SELECT CAST(SCOPE_IDENTITY() AS INT)", ingredient);
            }
        }

        public async Task Update(Ingredient ingredient)
        {
            using (var connection = await ConnectAsync())
            {
                await connection.ExecuteAsync(@"
                    UPDATE Ingredients SET
                        ExternalId = @ExternalId,
                        [Name] = @Name,
                        Category = @Category,
                        ImageUrl = @ImageUrl,
                        MeasurementType = @MeasurementType,
                        DensityMultiplier = @DensityMultiplier,
                        IsCertified = @IsCertified,
                        Calories = @Calories,
                        TotalFat = @TotalFat,
                        SaturatedFat = @SaturatedFat,
                        TransFat = @TransFat,
                        PolyunsaturatedFat = @PolyunsaturatedFat,
                        MonounsaturatedFat = @MonounsaturatedFat,
                        TotalCarbohydrate = @TotalCarbohydrate,
                        Sugar = @Sugar,
                        Fiber = @Fiber,
                        Protein = @Protein,
                        Cholesterol = @Cholesterol,
                        Sodium = @Sodium
                    WHERE
                        Id = @Id", ingredient); 
            }
		}

        public async Task Delete(int id)
        {
            using (var connection = await ConnectAsync())
            {
                await connection.ExecuteAsync("DELETE FROM Ingredients WHERE Id=@Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            using (var connection = await ConnectAsync())
            {
                return await connection.QueryAsync<string>(
                    "SELECT DISTINCT Category FROM Ingredients ORDER BY Category");
            }
        }
	}
 }