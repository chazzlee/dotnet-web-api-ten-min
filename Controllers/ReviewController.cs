using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenMin.DTOs;
using TenMin.Interfaces;
using TenMin.Models;

namespace TenMin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository reviewRepository;
    private readonly IMapper mapper;

    public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
    {
        this.reviewRepository = reviewRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllReviews()
    {
        var reviews = this.mapper.Map<List<ReviewDTO>>(this.reviewRepository.GetReviews());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(reviews);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetReviewById(int id)
    {
        if (!this.reviewRepository.ReviewExists(id))
        {
            return NotFound();
        }

        var review = this.mapper.Map<ReviewDTO>(this.reviewRepository.GetReview(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(review);
    }

    [HttpGet("pokemon/{pokemonId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetReviewsOfAPokemon(int pokemonId)
    {
        var reviews = this.mapper.Map<List<ReviewDTO>>(
            this.reviewRepository.GetReviewsOfAPokemon(pokemonId)
        );

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(reviews);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateReview([FromBody] ReviewDTO newReview)
    {
        if (newReview == null)
        {
            return BadRequest(ModelState);
        }

        var review = this.reviewRepository.GetReviews()
            .Where(r => r.Title.Trim().ToUpper() == newReview.Title.TrimEnd().ToUpper())
            .FirstOrDefault();

        if (review != null)
        {
            ModelState.AddModelError("name", "Review already exists");
            return UnprocessableEntity(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var reviewMap = this.mapper.Map<Review>(newReview);

        if (!this.reviewRepository.CreateReview(reviewMap))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        return Ok("Successfully created");
    }
}