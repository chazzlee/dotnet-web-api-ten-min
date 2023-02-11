using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;

namespace TenMin.Models;

public class Reviewer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public ICollection<Review> Reviews { get; set; } = new Collection<Review>();
}