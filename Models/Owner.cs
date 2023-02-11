using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TenMin.Models;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Gym { get; set; } = String.Empty;
    public Country Country { get; set; } = new Country();
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new Collection<PokemonOwner>();
}