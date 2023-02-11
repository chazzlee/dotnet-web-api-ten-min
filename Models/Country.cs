using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TenMin.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public ICollection<Owner> Owners { get; set; } = new Collection<Owner>();
}