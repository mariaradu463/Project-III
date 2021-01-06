
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using recipePickerApp.Models;
using recipePickerApp.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace recipePickerApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRecipeService recipeService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(UserManager<User> userManager,
            IUserService userService, IRecipeService recipeService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.recipeService = recipeService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult user_home()
        {
            var id = userManager.GetUserId(User);
            return View(userService.getUserById(id));
        }

        [HttpGet]
        public IActionResult UserOwnRecipes()
        {
            var id = userManager.GetUserId(User);
            IEnumerable<Recipe> model = userService.getRecipesForUser(id, "own");
            if (model == null)
            {
                return View();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOwnRecipes([FromForm] Recipe model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    model.UserId = userManager.GetUserId(User);
                    model.RecipeType = RecipeType.own;
                    userService.addRecipe(model);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("UserOwnRecipes", "User");
        }


        [HttpGet]
        public IActionResult UserFavoriteRecipes()
        {
            var id = userManager.GetUserId(User);
            var model = userService.getRecipesForUser(id, "favorite");
            return View(model);
        }

        public async Task<IActionResult> AddFavoriteRecipes(long recipeId)
        {
            var recipe = recipeService.GetRecipeById(recipeId);
            try
            {
                if (ModelState.IsValid)
                {
                    var newRecipe = new Recipe
                    {
                        Name = recipe.Name,
                        Category = recipe.Category,
                        CookingDecription = recipe.CookingDecription,
                        CookingTime = recipe.CookingTime,
                        Description = recipe.Description,
                        ImageUrl = recipe.ImageUrl,
                        RecipeIngredients = recipe.RecipeIngredients
                    };
                    newRecipe.UserId = userManager.GetUserId(User);
                    newRecipe.RecipeType = RecipeType.favorite;
                    userService.addRecipe(newRecipe);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("UserFavoriteRecipes", "User");
        }

        [HttpGet]
        public IActionResult UserCookedRecipes()
        {
            var id = userManager.GetUserId(User);
            IEnumerable<Recipe> model = userService.getRecipesForUser(id, "cooked");
            return View(model);
        }

        public async Task<IActionResult> AddCookedRecipes(long recipeId)
        {
            var recipe = recipeService.GetRecipeById(recipeId);
            try
            {
                if (ModelState.IsValid)
                {
                    var newRecipe = new Recipe
                    {
                        Name = recipe.Name,
                        Category = recipe.Category,
                        CookingDecription = recipe.CookingDecription,
                        CookingTime = recipe.CookingTime,
                        Description = recipe.Description,
                        ImageUrl = recipe.ImageUrl,
                        RecipeIngredients = recipe.RecipeIngredients,
                    };
                    newRecipe.UserId = userManager.GetUserId(User);
                    newRecipe.RecipeType = RecipeType.cooked;
                    userService.addRecipe(newRecipe);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("UserCookedRecipes", "User");
        }

    }
    
}

