using AutoMapper;
using TenMin.Models;
using TenMin.DTOs;

namespace TenMin.Utils;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pokemon, PokemonDTO>();
    }
}