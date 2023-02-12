using System.Collections.Generic;
using TenMin.Models;

namespace TenMin.Interfaces;

public interface IReviewerRepository
{
    public ICollection<Reviewer> GetReviewers();
    public Reviewer? GetReviewerById(int id);
    public ICollection<Review> GetReviewsByReviewer(int reviewerId);
    public bool ReviewerExists(int id);
    public bool Save();
}