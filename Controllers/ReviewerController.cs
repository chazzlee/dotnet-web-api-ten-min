using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenMin.DTOs;
using TenMin.Interfaces;

namespace TenMin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewerController : Controller
{
    private readonly IReviewerRepository reviewerRepository;
    private readonly IMapper mapper;

    public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
    {
        this.reviewerRepository = reviewerRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewerDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllReviewers()
    {
        var reviewers = this.mapper.Map<List<ReviewerDTO>>(this.reviewerRepository.GetReviewers());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(reviewers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewerDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetReviewerById(int id)
    {
        if (!this.reviewerRepository.ReviewerExists(id))
        {
            return NotFound();
        }

        var reviewer = this.mapper.Map<ReviewerDTO>(this.reviewerRepository.GetReviewerById(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(reviewer);
    }

    [HttpGet("{reviewerId}/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetReviewsByReviewer(int reviewerId)
    {
        if (!this.reviewerRepository.ReviewerExists(reviewerId))
        {
            return NotFound();
        }

        var reviews = this.mapper.Map<List<ReviewDTO>>(this.reviewerRepository.GetReviewsByReviewer(reviewerId));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(reviews);
    }
}