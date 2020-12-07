
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using recipePickerApp.Models;
using recipePickerApp.Service;
using System;
using System.Collections.Generic;
using System.IO;

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

        public IActionResult AddOwnRecipes()
        {
            return View();
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

    }
    
}

