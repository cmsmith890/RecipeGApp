using Microsoft.EntityFrameworkCore;

namespace RecipeGApp
{
    // Model
    public class Recipe
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
    }

    // DbContext
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }
        public DbSet<Recipe> Recipes { get; set; }
    }

    // Interface
    public interface IRecipeService
    {
        Task AddRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int id);
        Task<List<string>> GetCategoriesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task<List<Recipe>> GetRecipesAsync();
        Task UpdateRecipeAsync(Recipe recipe);
    }

    // Service Implementation
    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext _context;

        public RecipeService(RecipeDbContext context)
        {
            _context = context;
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            return await _context.Recipes
                .Select(r => r.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }
    }

    // Razor Page Component as String (for demonstration)
    public static class RecipeBlazorComponent
    {
        public static string GetComponentCode()
        {
            return @"@page "          }

        static readonly object? IRecipeService public static string? Recipes { get; private set; }

        RecipeService

    private class{
            public ()
            {
            // Razor Page Component as String (for demonstration)
                public static class RecipeBlazorComponent
            {
                public static string GetComponentCode()
                {
                    return @"
                            @page ""/recipes""
                            @inject IRecipeService RecipeService

                            <h3 class=""mt-4 mb-3"">Recipe List</h3>

                            @if (recipes == null)
                            {
                                <p>Loading...</p>
                            }
                            else
                            {
                                <ul>
                                    @foreach (var recipe in recipes)
                                    {
                                        <li>@recipe.Name (@recipe.Category)</li>
                                    }
                                </ul>
                            }";
                }
            }
        }
    }
}

string isRecipesLoading = "mt-4 mb-3\">Recipe List</h3>;= isRecipesLoading
object recipeCollection = @if(recipes
                              == null);

static object @if(bool v)
{
    throw new NotImplementedException();
}