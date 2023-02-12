using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TenMin.Interfaces;
using AutoMapper;
using TenMin.DTOs;
using TenMin.Models;

namespace TenMin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly IPokemonRepository pokemonRepository;
    private readonly IMapper mapper;

    public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
    {
        this.pokemonRepository = pokemonRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PokemonDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllPokemon()
    {
        var pokemons = this.mapper.Map<List<PokemonDTO>>(this.pokemonRepository.GetPokemons());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(pokemons);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PokemonDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPokemonById(int id)
    {
        if (!this.pokemonRepository.PokemonExists(id))
        {
            return NotFound();
        }

        var pokemon = this.mapper.Map<PokemonDTO>(this.pokemonRepository.GetPokemon(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(pokemon);
    }

    [HttpGet("{id}/rating")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPokemonRating(int id)
    {
        if (!this.pokemonRepository.PokemonExists(id))
        {
            return NotFound();
        }

        var rating = this.pokemonRepository.GetPokemonRating(id);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(rating);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreatePokemon(
        [FromQuery] int ownerId,
        [FromQuery] int categoryId,
        [FromBody] PokemonDTO newPokemon
    )
    {
        if (newPokemon == null)
        {
            return BadRequest(ModelState);
        }

        var pokemon = this.pokemonRepository.GetPokemons()
            .Where(p => p.Name.Trim().ToUpper() == newPokemon.Name.TrimEnd().ToUpper())
            .FirstOrDefault();

        if (pokemon != null)
        {
            ModelState.AddModelError("name", "Pokemon already exists");
            return UnprocessableEntity(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pokemonMap = this.mapper.Map<Pokemon>(newPokemon);

        if (!this.pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        return Ok("Successfully created");
    }
}