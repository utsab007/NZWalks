﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionReqDto>().ReverseMap();
            CreateMap<Region, UpdateRegionReqDto>().ReverseMap();
            CreateMap<Walk,AddWalkReqDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkReqDto>().ReverseMap();
        }
    }
}
