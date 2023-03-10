using System.Collections.Generic;
using System.Linq;
using TenMin.Data;
using TenMin.Interfaces;
using TenMin.Models;

namespace TenMin.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly DataContext context;

    public ReviewRepository(DataContext context)
    {
        this.context = context;
    }

    public bool CreateReview(Review review)
    {
        this.context.Reviews.Add(review);
        return Save();
    }

    public Review? GetReview(int id)
    {
        return this.context.Reviews.Where(r => r.Id == id).FirstOrDefault();
    }

    public ICollection<Review> GetReviews()
    {
        return this.context.Reviews.ToList();
    }

    public ICollection<Review> GetReviewsOfAPokemon(int pokemonId)
    {
        return this.context.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();
    }

    public bool ReviewExists(int id)
    {
        return this.context.Reviews.Any(r => r.Id == id);
    }
    public bool Save()
    {
        var saved = this.context.SaveChanges();
        return saved > 0 ? true : false;
    }
}