using Microsoft.AspNetCore.Identity;
using recipePickerApp.ManyToManyModel;
using System;
using System.Collections.Generic;

namespace recipePickerApp.Models
{
    public class User : IdentityUser
    {
        public ICollection<Recipe> OwnRecipes { get; set; }
        
    }
}
