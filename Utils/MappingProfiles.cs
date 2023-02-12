using AutoMapper;
using TenMin.Models;
using TenMin.DTOs;

namespace TenMin.Utils;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pokemon, PokemonDTO>();
        CreateMap<Category, CategoryDTO>();
        CreateMap<Country, CountryDTO>();
        CreateMap<Owner, OwnerDTO>();
        CreateMap<Review, ReviewDTO>();
    }
}