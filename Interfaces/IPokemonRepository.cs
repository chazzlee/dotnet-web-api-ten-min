using System.Collections.Generic;
using TenMin.Models;

namespace TenMin.Interfaces;

public interface IPokemonRepository
{
    ICollection<Pokemon> GetPokemons();
}