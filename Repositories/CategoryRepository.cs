using System.Collections.Generic;
using TenMin.Models;
using TenMin.Interfaces;
using TenMin.Data;
using System.Linq;

namespace TenMin.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext context;

    public CategoryRepository(DataContext context)
    {
        this.context = context;
    }

    public bool CategoryExists(int id)
    {
        return this.context.Categories.Any(c => c.Id == id);
    }

    public bool CreateCategory(Category category)
    {
        this.context.Categories.Add(category);

        return Save();
    }

    public ICollection<Category> GetCategories()
    {
        return this.context.Categories.ToList();
    }

    public Category? GetCategory(int id)
    {
        return this.context.Categories.Where(c => c.Id == id).FirstOrDefault();
    }

    public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
    {
        return this.context.PokemonCategories
            .Where(pc => pc.CategoryId == categoryId)
            .Select(c => c.Pokemon)
            .ToList();
    }

    public bool Save()
    {
        var saved = this.context.SaveChanges();
        return saved > 0 ? true : false;
    }
}