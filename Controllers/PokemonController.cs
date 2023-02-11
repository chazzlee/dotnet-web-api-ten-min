using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TenMin.Interfaces;
using TenMin.Models;
using AutoMapper;
using TenMin.DTOs;

namespace TenMin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : Controller
{
    private readonly IPokemonRepository pokemonRepository;
    private readonly IMapper mapper;

    public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
    {
        this.pokemonRepository = pokemonRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
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
}