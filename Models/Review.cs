using System;

namespace TenMin.Models;

public class Review
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Body { get; set; } = String.Empty;
}