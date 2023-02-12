using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenMin.DTOs;
using TenMin.Interfaces;

namespace TenMin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : Controller
{
    private readonly ICountryRepository countryRepository;
    private readonly IMapper mapper;

    public CountryController(ICountryRepository countryRepository, IMapper mapper)
    {
        this.countryRepository = countryRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CountryDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllCountries()
    {
        var countries = this.mapper.Map<List<CountryDTO>>(this.countryRepository.GetCountries());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(countries);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCountry(int id)
    {
        if (!this.countryRepository.CountryExists(id))
        {
            return NotFound();
        }

        var country = this.mapper.Map<CountryDTO>(this.countryRepository.GetCountry(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(country);
    }

    [HttpGet("owners/{ownerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCountryOfAnOwner(int ownerId)
    {
        var country = this.mapper.Map<CountryDTO>(this.countryRepository.GetCountryByOwner(ownerId));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(country);
    }
}