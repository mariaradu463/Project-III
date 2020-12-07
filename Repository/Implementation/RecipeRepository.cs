using recipePickerApp.DataContext;
using recipePickerApp.Models;
using recipePickerApp.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace recipePickerApp.Repository
{
    public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
    {
        
        public RecipeRepository(UserContext repositoryContext)
           : base(repositoryContext)
        {
          
        }

        public IEnumerable<Recipe> GetAll()
        {
            RecipeType recipeType = RecipeType.own;
            return userContext.Recipes
                    .Where(r => r.RecipeType.Equals(recipeType))
                    .AsEnumerable();
        }

        public ICollection<Recipe> FindByCategory(string category)
        {
            Category category1 = (Category)System.Enum.Parse(typeof(Category), category);
            RecipeType recipeType = RecipeType.own;
            ICollection<Recipe> recipes= userContext.Recipes
                .Where(r => r.Category.Equals(category1) && r.RecipeType.Equals(recipeType))
                .ToList();
            return recipes;
        }

        public IQueryable<string> FindAllCategoriesAsString()
        {
            IQueryable<string> categoryQuery = from m in userContext.Recipes
                                               select m.Category.ToString();
            return categoryQuery;
        }

        public IEnumerable<Recipe> GetOwnRecipesForUser(string id)
        {
            RecipeType recipeType = RecipeType.own;
            return userContext.Recipes
                 .Where(recipe => recipe.UserId.Equals(id) && (recipe.RecipeType.Equals(recipeType)))
                 .AsEnumerable();
        }
    }
}
