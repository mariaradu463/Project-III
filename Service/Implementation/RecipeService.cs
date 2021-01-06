using recipePickerApp.Exceptions;
using recipePickerApp.Models;
using recipePickerApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace recipePickerApp.Service.Implementation
{

    public class RecipeService : IRecipeService
    {
       public IRepositoryWrapper _repositoryWrapper;
      
       public RecipeService(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        public IEnumerable<Recipe> getAllRecipes()
        {
           
            return _repositoryWrapper.Recipe.GetAll();
        }

        public ICollection<Ingredient> getIngredientsForRecipe(long Id)
        {
            if (Id == 0)
            {
                throw new Exception("Recipe id is null");
            }
            var ingredients= _repositoryWrapper.Ingredient.GetIngredientsForRecipe(Id);

            if (ingredients == null)
            {
                throw new EntitiesNotFoundException("No ingredients for recipe");
            }
            return ingredients;
        }

        public ICollection<Ingredient> addIngredientsToRecipe(ICollection<Ingredient> ingredients, long recipeId)
        {

            return _repositoryWrapper.Ingredient.AddIngredientsToRecipe(ingredients, recipeId);
        }

        public Recipe GetRecipeById(long id)
        {
            if (id == 0)
            {
                throw new Exception("Id parameter is null");
            }

            var recipe= _repositoryWrapper.Recipe.findById(id);

            if(recipe==null)
            {
                throw new EntityNotFoundException(id);
            }
            return recipe;
        }

        public Recipe AddRecipe(Recipe recipe)
        {
            return _repositoryWrapper.Recipe.Add(recipe);
        }

        public ICollection<Recipe> GetRecipesByCategory(string category)
        {
            if(category==null)
            {
                throw new Exception("Category parameter is null");
            }
            var recipes= _repositoryWrapper.Recipe.FindByCategory(category);
            if(recipes==null)
            {
                throw new EntitiesNotFoundException("No recipes for this category");
            }
            return recipes;
        }
        public IQueryable<string> getAllCategoriesAsString()
        {
           return _repositoryWrapper.Recipe.FindAllCategoriesAsString();
        }

        public Recipe Update(Recipe recipe)
        {
            return _repositoryWrapper.Recipe.Update(recipe);
        }

        public IEnumerable<Recipe> GetRecipesByCategoryAndName(string category, string name)
        {
            if (category != null && name != null)
            {
                return _repositoryWrapper.Recipe.FindByNameAndCategory(category, name);
            }
            if (category == null && name == null)
            {
                return _repositoryWrapper.Recipe.GetAll();
            }
            if (category == null && name != null)
            {
                return _repositoryWrapper.Recipe.FindByName(name);
            }
            if (category != null && name == null)
            {
                return _repositoryWrapper.Recipe.FindByCategory(category);
            }
            return _repositoryWrapper.Recipe.FindAll();
        }
    }
}
