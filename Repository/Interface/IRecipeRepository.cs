using recipePickerApp.Models;
using recipePickerApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace recipePickerApp.Repository
{
    public  interface IRecipeRepository : IRepositoryBase<Recipe>
    {
        IEnumerable<Recipe> GetAll();
        ICollection<Recipe> FindByCategory(string category);
        IQueryable<String> FindAllCategoriesAsString();
        IEnumerable<Recipe> GetOwnRecipesForUser(string id);
    }
}
