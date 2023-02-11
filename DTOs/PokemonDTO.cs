using System;

namespace TenMin.DTOs;

public class PokemonDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime BirthDate { get; set; }
}