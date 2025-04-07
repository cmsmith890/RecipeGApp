using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipeGApp.RecipeGApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register EF Core with SQLite (or use another provider)
        builder.Services.AddDbContext<RecipeDbContext>(static options =>
            options.UseSqlite("Data Source=recipes.db"));

        // Register RecipeService as scoped
        builder.Services.AddScoped<IRecipeService, RecipeService>();

        // Add Razor Pages and Blazor Server
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        // Run migrations and ensure DB is created at startup
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
            dbContext.Database.Migrate();
        }

        // Configure HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}