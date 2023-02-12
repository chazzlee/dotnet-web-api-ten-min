using System.Collections;
using System.Collections.Generic;
using TenMin.Models;

namespace TenMin.Interfaces;

public interface IReviewRepository
{
    public ICollection<Review> GetReviews();
    public Review? GetReview(int id);
    public ICollection<Review> GetReviewsOfAPokemon(int pokemonId);
    public bool ReviewExists(int id);
    public bool Save();
}