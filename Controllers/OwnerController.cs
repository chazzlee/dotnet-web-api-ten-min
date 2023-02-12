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
public class OwnerController : Controller
{
    private readonly IOwnerRepository ownerRepository;
    private readonly IMapper mapper;

    public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
    {
        this.ownerRepository = ownerRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OwnerDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetOwners()
    {
        var owners = this.mapper.Map<List<OwnerDTO>>(this.ownerRepository.GetOwners());

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(owners);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OwnerDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetOwnerById(int id)
    {
        if (!this.ownerRepository.OwnerExists(id))
        {
            return NotFound();
        }

        var owner = this.mapper.Map<OwnerDTO>(this.ownerRepository.GetOwner(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(owner);
    }

    [HttpGet("{ownerId}/pokemon")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PokemonDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPokemonByOwner(int ownerId)
    {
        if (!this.ownerRepository.OwnerExists(ownerId))
        {
            return NotFound();
        }

        var owner = this.mapper.Map<List<PokemonDTO>>(this.ownerRepository.GetPokemonByOwner(ownerId));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(owner);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateOwner([FromBody] OwnerDTO newOwner)
    {
        if (newOwner == null)
        {
            return BadRequest(ModelState);
        }

        var owner = this.ownerRepository.GetOwners()
            .Where(o => o.LastName.Trim().ToUpper() == newOwner.LastName.TrimEnd().ToUpper())
            .FirstOrDefault();

        if (owner != null)
        {
            ModelState.AddModelError("name", "Owner already exists");
            return UnprocessableEntity(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var ownerMap = this.mapper.Map<Owner>(newOwner);
        if (!this.ownerRepository.CreateOwner(ownerMap))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        return Ok("Successfully created");
    }
}