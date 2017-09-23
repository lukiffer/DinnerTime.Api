using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DinnerTime.Api.Models;

namespace DinnerTime.Api.Repositories
{
	public class RecipeRepository : RepositoryBase
	{
		public async Task<IEnumerable<RecipeBase>> GetAll()
		{
			using (var connection = await ConnectAsync())
			{
				return await connection.QueryAsync<RecipeBase>(@"
                    SELECT
                        r.Id,
                        r.UserId,
                        u.[Name] AS [UserName],
                        r.[Name],
                        r.Category,
                        r.ImageUrl,
                        r.Instructions,
                        r.LastUpdated,
                        r.Servings,
                        r.Calories,
                        r.TotalFat,
                        r.TotalCarbohydrate,
                        r.Protein
                    FROM
                        Recipes r
                        LEFT OUTER JOIN Users u ON u.Id = r.UserId");
			}
		}

		public async Task<Recipe> Get(int id)
		{
			using (var connection = await ConnectAsync())
			{
				return (await connection.QueryAsync<Recipe>(@"
                    SELECT
                        r.Id,
                        r.UserId,
                        u.[Name] AS [UserName],
                        r.[Name],
                        r.Category,
                        r.ImageUrl,
                        r.Instructions,
                        r.LastUpdated,
                        r.Servings,
                        r.Calories,
                        r.TotalFat,
                        r.TotalCarbohydrate,
                        r.Protein
                    FROM
                        Recipes r
                        LEFT OUTER JOIN Users u ON u.Id = r.UserId
                    WHERE
                        r.Id = @Id",
					new { Id = id })).SingleOrDefault();
			}
		}

		public async Task<int> Create(RecipeBase recipe)
		{
			using (var connection = await ConnectAsync())
			{
				return await connection.ExecuteScalarAsync<int>(@"
                    INSERT INTO Recipes (
                        UserId,
                        [Name],
                        Category,
                        ImageUrl,
                        Instructions,
                        LastUpdated,
                        Servings,
                        Calories,
                        TotalFat,
                        TotalCarbohydrate,
                        Protein
                    )
                    VALUES (
                        @UserId,
                        @Name,
                        @Category,
                        @ImageUrl,
                        @Instructions,
                        @LastUpdated,
                        @Servings,
                        @Calories,
                        @TotalFat,
                        @TotalCarbohydrate,
                        @Protein
                    )
                    SELECT CAST(SCOPE_IDENTITY() AS INT)", recipe);
			}
		}

		public async Task Update(RecipeBase recipe)
		{
			using (var connection = await ConnectAsync())
			{
				await connection.ExecuteAsync(@"
                    UPDATE Recipes SET
                        UserId = @UserId,
                        [Name] = @Name,
                        Category = @Category,
                        ImageUrl = @ImageUrl,
                        Instructions = @Instructions,
                        LastUpdated = @LastUpdated,
                        Servings = @Servings,
                        Calories = @Calories,
                        TotalFat = @TotalFat,
                        TotalCarbohydrate = @TotalCarbohydrate,
                        Protein = @Protein
                    WHERE
                        Id=@Id", recipe);
			}
		}

        public async Task Delete(int id)
        {
            using (var connection = await ConnectAsync())
            {
                await connection.ExecuteAsync("DELETE FROM Recipes WHERE Id=@Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<RecipeIngredient>> GetRecipeIngredients(int recipeId)
        {
            using (var connection = await ConnectAsync())
            {
                return await connection.QueryAsync<RecipeIngredient>(@"
                    SELECT
                        *
                    FROM
                        RecipeIngredients ri
                        INNER JOIN Ingredients i ON i.Id = ri.IngredientId
                    WHERE
                        ri.RecipeId = @RecipeId", new { RecipeId = recipeId });
            }
        }

        public async Task CreateRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            using (var connection = await ConnectAsync())
            {
                await connection.ExecuteAsync(@"
                    INSERT INTO RecipeIngredients (
                        RecipeId,
                        IngredientId,
                        Quantity,
                        Units
                    )
                    VALUES (
                        @RecipeId,
                        @IngredientId,
                        @Quantity,
                        @Units
                    )", recipeIngredient);
            }
        }

        public async Task UpdateRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            using (var connection = await ConnectAsync())
            {
                await connection.ExecuteAsync(@"
                    UPDATE RecipeIngredients SET
                        Quantity = @Quantity,
                        Units = @Units
                    WHERE
                        RecipeId = @RecipeId AND
                        IngredientId = @IngredientId", recipeIngredient);
            }
        }

        public async Task DeleteRecipeIngredient(RecipeIngredient recipeIngredient)
        {
			using (var connection = await ConnectAsync())
			{
                await connection.ExecuteAsync(
                    "DELETE FROM RecipeIngredients WHERE RecipeId=@RecipeId AND IngredientId=@IngredientId", recipeIngredient);
			}
        }

		public async Task<IEnumerable<string>> GetCategories()
		{
			using (var connection = await ConnectAsync())
			{
				return await connection.QueryAsync<string>(
					"SELECT DISTINCT Category FROM Recipes ORDER BY Category");
			}
		}
	}
}