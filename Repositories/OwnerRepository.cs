using System.Collections.Generic;
using System.Linq;
using TenMin.Data;
using TenMin.Interfaces;
using TenMin.Models;

namespace TenMin.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly DataContext context;

    public OwnerRepository(DataContext context)
    {
        this.context = context;
    }

    public ICollection<Owner> GetOwners()
    {
        return this.context.Owners.ToList();
    }

    public Owner? GetOwner(int id)
    {
        return this.context.Owners.Where(o => o.Id == id).FirstOrDefault();
    }

    public ICollection<Owner> GetOwnerOfAPokemon(int pokemonId)
    {
        return this.context.PokemonOwners
            .Where(po => po.PokemonId == pokemonId)
            .Select(o => o.Owner)
            .ToList();
    }

    public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
    {
        return this.context.PokemonOwners
            .Where(p => p.Owner.Id == ownerId)
            .Select(p => p.Pokemon)
            .ToList();
    }

    public bool OwnerExists(int ownerId)
    {
        return this.context.Owners.Any(o => o.Id == ownerId);
    }
}