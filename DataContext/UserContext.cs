using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using recipePickerApp.ManyToManyModel;
using recipePickerApp.Models;

namespace recipePickerApp.DataContext
{
    public class UserContext : IdentityDbContext<User>
    {

       public UserContext(DbContextOptions<UserContext> options)
            :base(options)
        {

        }
       
        public DbSet<Recipe> OwnRecipes { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<RecipeIngredients> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredients>().HasKey(sc => new { sc.IngredientId, sc.RecipeId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
