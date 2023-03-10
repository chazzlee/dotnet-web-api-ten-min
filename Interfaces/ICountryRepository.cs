using System.Collections.Generic;
using TenMin.Models;

namespace TenMin.Interfaces;

public interface ICountryRepository
{
    public ICollection<Country> GetCountries();
    public Country? GetCountry(int id);
    public Country? GetCountryByOwner(int ownerId);
    public ICollection<Owner> GetOwnersFromCountry(int countryId);
    public bool CountryExists(int id);
    public bool CreateCountry(Country country);
    public bool UpdateCountry(Country country);
    public bool Save();
}