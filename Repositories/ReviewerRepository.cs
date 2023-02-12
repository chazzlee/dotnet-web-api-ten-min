using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TenMin.Data;
using TenMin.Interfaces;
using TenMin.Models;

namespace TenMin.Repositories;

public class ReviewerRepository : IReviewerRepository
{
    private readonly DataContext context;

    public ReviewerRepository(DataContext context)
    {
        this.context = context;
    }

    public bool CreateReviewer(Reviewer reviewer)
    {
        this.context.Reviewers.Add(reviewer);
        return Save();
    }

    public Reviewer? GetReviewerById(int id)
    {
        return this.context.Reviewers
            .Where(r => r.Id == id)
            .Include(e => e.Reviews)
            .FirstOrDefault();
    }

    public ICollection<Reviewer> GetReviewers()
    {
        return this.context.Reviewers.Include(r => r.Reviews).ToList();
    }

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
        return this.context.Reviews
            .Where(r => r.Reviewer.Id == reviewerId)
            .ToList();
    }

    public bool ReviewerExists(int id)
    {
        return this.context.Reviewers.Any(r => r.Id == id);
    }

    public bool Save()
    {
        var saved = this.context.SaveChanges();
        return saved > 0 ? true : false;
    }
}