using System;

namespace TenMin.DTOs;

public class ReviewDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Body { get; set; } = String.Empty;
    public int Rating { get; set; }
}