using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TenMin.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime BirthDate { get; set; }
    public ICollection<Review> Reviews { get; set; } = new Collection<Review>();
}