using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenMin.DTOs;
using TenMin.Interfaces;
using TenMin.Models;

namespace TenMin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCategories()
    {
        var categories = this.mapper.Map<List<CategoryDTO>>(this.categoryRepository.GetCategories());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(categories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCategory(int id)
    {
        if (!this.categoryRepository.CategoryExists(id))
        {
            return NotFound();
        }

        var category = this.mapper.Map<CategoryDTO>(this.categoryRepository.GetCategory(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(category);
    }

    [HttpGet("{id}/pokemon")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PokemonDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPokemonByCategory(int id)
    {
        var pokemon = this.mapper.Map<List<PokemonDTO>>(
            this.categoryRepository.GetPokemonByCategory(id)
        );

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(pokemon);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateCategory([FromBody] CategoryDTO newCategory)
    {
        if (newCategory == null)
        {
            return BadRequest(ModelState);
        }

        var category = this.categoryRepository.GetCategories()
            .Where(c => c.Name.Trim().ToUpper() == newCategory.Name.TrimEnd().ToUpper())
            .FirstOrDefault();

        if (category != null)
        {
            ModelState.AddModelError("name", "Category already exists");
            return UnprocessableEntity(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoryMap = this.mapper.Map<Category>(newCategory);
        if (!this.categoryRepository.CreateCategory(categoryMap))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        return Ok("Successfully created");
    }
}