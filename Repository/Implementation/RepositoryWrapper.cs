using recipePickerApp.DataContext;
using recipePickerApp.Repository.Interface;

namespace recipePickerApp.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private UserContext _dbContext;
        private RecipeRepository _recipeRepository;
        private IngredientRepository _ingredientRepository;
        private UserRepository _userRepository;


        public RepositoryWrapper(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRecipeRepository Recipe {
            get
            {
                if(_recipeRepository==null)
                {
                    _recipeRepository = new RecipeRepository(_dbContext);
                }
                return _recipeRepository;
            }
        }
        public IIngredientRepository Ingredient
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new IngredientRepository(_dbContext);
                }
                return _ingredientRepository;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dbContext);
                }
                return _userRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
