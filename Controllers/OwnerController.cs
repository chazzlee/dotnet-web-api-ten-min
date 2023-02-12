using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenMin.DTOs;
using TenMin.Interfaces;

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
}