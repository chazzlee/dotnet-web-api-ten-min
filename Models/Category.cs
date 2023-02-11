using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TenMin.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new Collection<PokemonCategory>();
}