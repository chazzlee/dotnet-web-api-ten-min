using System;

namespace TenMin.DTOs;

public class OwnerDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Gym { get; set; } = String.Empty;
}