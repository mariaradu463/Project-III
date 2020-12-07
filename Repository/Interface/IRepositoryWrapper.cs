namespace recipePickerApp.Repository.Interface
{
    public interface IRepositoryWrapper
    {
        IRecipeRepository Recipe { get; }
        IIngredientRepository Ingredient { get; }
        IUserRepository User { get; }
        void Save();
    }
}
