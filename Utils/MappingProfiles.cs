using AutoMapper;
using TenMin.Models;
using TenMin.DTOs;

namespace TenMin.Utils;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pokemon, PokemonDTO>();
        CreateMap<PokemonDTO, Pokemon>();
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
        CreateMap<Country, CountryDTO>();
        CreateMap<CountryDTO, Country>();
        CreateMap<Owner, OwnerDTO>();
        CreateMap<OwnerDTO, Owner>();
        CreateMap<Review, ReviewDTO>();
        CreateMap<ReviewDTO, Review>();
        CreateMap<Reviewer, ReviewerDTO>();
        CreateMap<ReviewerDTO, Reviewer>();
    }
}