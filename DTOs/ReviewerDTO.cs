using System;
using System.Collections.Generic;

namespace TenMin.DTOs;

public class ReviewerDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public ICollection<ReviewDTO>? Reviews { get; set; } = null;
}