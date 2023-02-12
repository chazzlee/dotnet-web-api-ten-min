using System.Collections.Generic;
using System.Linq;
using TenMin.Interfaces;
using TenMin.Models;
using TenMin.Data;

namespace TenMin.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly DataContext _context;

    public PokemonRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Pokemon> GetPokemons()
    {
        return _context.Pokemon.OrderBy(p => p.Id).ToList();
    }

    public Pokemon? GetPokemon(int id)
    {
        return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
    }

    public Pokemon? GetPokemon(string name)
    {
        return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
    }

    public decimal GetPokemonRating(int pokemonId)
    {
        var review = _context.Reviews.Where(r => r.Pokemon.Id == pokemonId);
        if (review.Count() <= 0)
        {
            return 0;
        }

        return ((decimal)review.Sum(r => r.Rating) / review.Count());
    }

    public bool PokemonExists(int pokemonId)
    {
        return _context.Pokemon.Any(p => p.Id == pokemonId);
    }

    public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
    {
        var owner = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
        var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();
        if (owner == null || category == null)
        {
            return false;
        }

        var pokemonOwner = new PokemonOwner()
        {
            Owner = owner,
            Pokemon = pokemon
        };

        _context.PokemonOwners.Add(pokemonOwner);
        var pokemonCategory = new PokemonCategory()
        {
            Category = category,
            Pokemon = pokemon
        };
        _context.PokemonCategories.Add(pokemonCategory);
        _context.Pokemon.Add(pokemon);

        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}