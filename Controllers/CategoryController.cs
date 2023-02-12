using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenMin.DTOs;
using TenMin.Interfaces;

namespace TenMin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
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

    [HttpGet("pokemon/{categoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PokemonDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCategoryById(int categoryId)
    {
        var pokemon = this.mapper.Map<List<PokemonDTO>>(
            this.categoryRepository.GetPokemonByCategory(categoryId)
        );

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(pokemon);
    }
}