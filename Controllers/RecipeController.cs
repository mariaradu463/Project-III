using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using recipePickerApp.Models;
using recipePickerApp.Service;
namespace recipePickerApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService service;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RecipeController(IRecipeService service,IWebHostEnvironment webHostEnvironment)
        {
           this.service=service;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Recipe> model = service.getAllRecipes();
            return View(model);
        }
        [Authorize]
        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRecipe([Bind("RecipeId,Name,Description,ImageUrl,CookingDecription,CookingTime,Category,RecipeType")]Recipe model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.AddRecipe(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Details(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            RecipeViewModel _recipeViewModel = new RecipeViewModel();

            var recipe = service.GetRecipeById((long)id);
            ICollection<Ingredient> ingredients = service.getIngredientsForRecipe((long)id);

            _recipeViewModel.recipe = recipe;
            _recipeViewModel.ingredients = ingredients;
            if (recipe == null)
            {
                return NotFound();
            }

            return View(_recipeViewModel);
        }
        [HttpGet]
        public IActionResult CategoryRecipes(string category)
        {
            ICollection<Recipe> recipes = service.GetRecipesByCategory(category);
            return View(recipes);
        }

        public IActionResult Category()
        {
            return View();
        }
    }
}
