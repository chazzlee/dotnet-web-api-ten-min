using System;

namespace TenMin.Models;

public class Review
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Body { get; set; } = String.Empty;
    public Reviewer Reviewer { get; set; } = new Reviewer();
    public Pokemon Pokemon { get; set; } = new Pokemon();
}