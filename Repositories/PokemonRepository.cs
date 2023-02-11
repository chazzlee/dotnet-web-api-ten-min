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
}