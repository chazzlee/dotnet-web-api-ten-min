using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenMin.DTOs;
using TenMin.Interfaces;

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
}