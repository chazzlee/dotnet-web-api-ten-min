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
public class ReviewerController : ControllerBase
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateReviewer([FromBody] ReviewerDTO newReviewer)
    {
        if (newReviewer == null)
        {
            return BadRequest(ModelState);
        }

        var reviewer = this.reviewerRepository.GetReviewers()
            .Where(r => r.LastName.Trim().ToUpper() == newReviewer.LastName.TrimEnd().ToUpper())
            .FirstOrDefault();

        if (reviewer != null)
        {
            ModelState.AddModelError("name", "Reviewer already exists");
            return UnprocessableEntity(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var reviewerMap = this.mapper.Map<Reviewer>(newReviewer);
        if (!this.reviewerRepository.CreateReviewer(reviewerMap))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        return Ok("Successfully created");
    }
}