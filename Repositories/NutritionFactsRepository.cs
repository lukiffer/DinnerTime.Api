using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DinnerTime.Api.Models;

namespace DinnerTime.Api.Repositories
{
    public class NutritionFactsRepository : RepositoryBase
    {
		public async Task<IEnumerable<NutritionFacts>> GetAll()
		{
			using (var connection = await ConnectAsync())
			{
				return await connection.QueryAsync<NutritionFacts>("SELECT * FROM NutritionFacts");
			}
		}

		public async Task<NutritionFacts> Get(int id)
		{
			using (var connection = await ConnectAsync())
			{
				return (await connection.QueryAsync<NutritionFacts>(
					"SELECT * FROM NutritionFacts WHERE Id=@Id", new { Id = id }))
						.SingleOrDefault();
			}
		}

        public async Task<NutritionFacts> GetByIngredientId(int ingredientId)
        {
            using (var connection = await ConnectAsync())
            {
				return (await connection.QueryAsync<NutritionFacts>(
					"SELECT * FROM NutritionFacts WHERE IngredientId=@IngredientId", new { IngredientId = ingredientId }))
						.SingleOrDefault();
            }
        }

		public async Task<int> Create(NutritionFacts facts)
		{
			using (var connection = await ConnectAsync())
			{
				return await connection.ExecuteScalarAsync<int>(@"
                    INSERT INTO NutritionFacts (IngredientId, ServingSize, Calories, TotalFat, SaturatedFat, TransFat, PolyunsaturatedFat, MonounsaturatedFat, TotalCarbohydrate, Sugar, Fiber, Protein, Cholesterol, Sodium)
                    VALUES(@IngredientId, @ServingSize, @Calories, @TotalFat, @SaturatedFat, @TransFat, @PolyunsaturatedFat, @MonounsaturatedFat, @TotalCarbohydrate, @Sugar, @Fiber, @Protein, @Cholesterol, @Sodium)
                    SELECT CAST(SCOPE_IDENTITY() AS INT)", facts);
			}
		}

		public async Task Update(NutritionFacts facts)
		{
			using (var connection = await ConnectAsync())
			{
                await connection.ExecuteAsync(@"UPDATE NutritionFacts
                    SET 
                        ServingSize=@ServingSize,
                        Calories=@Calories,
                        TotalFat=@TotalFat,
                        SaturatedFat=@SaturatedFat,
                        TransFat=@TransFat,
                        PolyunsaturatedFat=@PolyunsaturatedFat,
                        MonounsaturatedFat=@MonounsaturatedFat,
                        TotalCarbohydrate=@TotalCarbohydrate,
                        Sugar=@Sugar,
                        Fiber=@Fiber,
                        Protein=@Protein,
                        Cholesterol=@Cholesterol,
                        Sodium=@Sodium
                    WHERE
                        IngredientId=@IngredientId", facts);
			}
		}

		public async Task Delete(int id)
		{
			using (var connection = await ConnectAsync())
			{
				await connection.ExecuteAsync("DELETE FROM NutritionFacts WHERE Id=@Id", new { Id = id });
			}
		}
    }
}
