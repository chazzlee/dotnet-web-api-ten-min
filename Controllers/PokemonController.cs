using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TenMin.Interfaces;
using TenMin.Models;

namespace TenMin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : Controller
{
    private readonly IPokemonRepository pokemonRepository;

    public PokemonController(IPokemonRepository pokemonRepository)
    {
        this.pokemonRepository = pokemonRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult Index()
    {
        var pokemons = this.pokemonRepository.GetPokemons();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(pokemons);
    }
}