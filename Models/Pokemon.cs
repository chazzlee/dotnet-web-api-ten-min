using System;

namespace TenMin.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime BirthDate { get; set; }
}