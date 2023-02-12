using System.Collections.Generic;
using TenMin.Models;

namespace TenMin.Interfaces;

public interface IOwnerRepository
{
    public ICollection<Owner> GetOwners();
    public Owner? GetOwner(int id);
    public ICollection<Owner> GetOwnerOfAPokemon(int pokemonId);
    public ICollection<Pokemon> GetPokemonByOwner(int ownerId);
    public bool OwnerExists(int ownerId);
    public bool CreateOwner(Owner owner);
    public bool Save();
}