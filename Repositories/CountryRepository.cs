using System.Collections.Generic;
using System.Linq;
using TenMin.Data;
using TenMin.Interfaces;
using TenMin.Models;

namespace TenMin.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext context;

    public CountryRepository(DataContext context)
    {
        this.context = context;
    }

    public bool CountryExists(int id)
    {
        return this.context.Countries.Any(c => c.Id == id);
    }

    public ICollection<Country> GetCountries()
    {
        return this.context.Countries.ToList();
    }

    public Country? GetCountry(int id)
    {
        return this.context.Countries.Where(c => c.Id == id).FirstOrDefault();
    }

    public Country? GetCountryByOwner(int ownerId)
    {
        return this.context.Owners
            .Where(o => o.Country.Id == ownerId)
            .Select(c => c.Country)
            .FirstOrDefault();
    }

    public ICollection<Owner> GetOwnersFromCountry(int countryId)
    {
        return this.context.Owners
            .Where(o => o.Country.Id == countryId)
            .ToList();
    }
}