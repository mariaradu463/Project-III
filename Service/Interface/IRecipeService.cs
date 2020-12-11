using recipePickerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace recipePickerApp.Service
{
    public interface IRecipeService
    {
        IEnumerable<Recipe> getAllRecipes();

        Recipe GetRecipeById(long id);

        Recipe AddRecipe(Recipe recipe);

        ICollection<Ingredient> getIngredientsForRecipe(long id);

        ICollection<Recipe> GetRecipesByCategory(string category);

        ICollection<Ingredient> addIngredientsToRecipe(ICollection<Ingredient> ingredients,long recipeId);

        IQueryable<String> getAllCategoriesAsString();
        Recipe Update(Recipe recipe);

    }

}
