using System.Collections.Generic;
using TenMin.Models;

namespace TenMin.Interfaces;

public interface ICategoryRepository
{
    public ICollection<Category> GetCategories();
    public Category? GetCategory(int id);
    public ICollection<Pokemon> GetPokemonByCategory(int categoryId);
    public bool CategoryExists(int id);
    public bool CreateCategory(Category category);
}