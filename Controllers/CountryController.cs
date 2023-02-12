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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateCountry([FromBody] CountryDTO newCountry)
    {
        if (newCountry == null)
        {
            return BadRequest(ModelState);
        }

        var countries = this.countryRepository.GetCountries()
            .Where(c => c.Name.Trim().ToUpper() == newCountry.Name.TrimEnd().ToUpper())
            .FirstOrDefault();

        if (countries != null)
        {
            ModelState.AddModelError("name", "Country already exists");
            return UnprocessableEntity(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var countryMap = this.mapper.Map<Country>(newCountry);
        if (!this.countryRepository.CreateCountry(countryMap))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        return Ok("Successfully created");
    }
}