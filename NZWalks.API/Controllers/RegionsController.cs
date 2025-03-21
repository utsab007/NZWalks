﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,
                        IMapper mapper,ILogger<RegionsController> logger)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("/test")]
        public IActionResult Test()
        {
            return Ok("Regions Controller Test for absolute path");
        }

        // GET all regions
        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
            //_logger.LogInformation("GetAll Regions action method was invoked");
            //_logger.LogWarning("test warning");
            // Get data from database - Regions domain table
            var regions = await _regionRepository.GetAllAsync();

            //var regionsDto = new List<RegionDto>();
            //// Map domain model to DTO
            //foreach (var region in regions)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //};

            // Use AutoMapper to map domain model to DTO
            var regionsDto = _mapper.Map<List<RegionDto>>(regions);

            //_logger.LogInformation($"Finished GetAll action method with region data : {JsonSerializer.Serialize(regionsDto)}");

            //throw new Exception("This is a new expection");

            return Ok(regionsDto);
        }

        // GET region by id
        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            var region = await _regionRepository.GetRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            // Map domain model to DTO
            //var regionDto = new RegionDto()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        // POST to create new region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody]AddRegionReqDto region)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            // Map DTO to domain model
            //var newRegion = new Region()
            //{
            //    Id = Guid.NewGuid(),
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var newRegion = _mapper.Map<Region>(region);
            //await _dbContext.Regions.AddAsync(newRegion);
            //await _dbContext.SaveChangesAsync();
            newRegion = await _regionRepository.CreateRegionAsync(newRegion);

            // Map domain model to DTO
            var regionDto = new RegionDto()
            {
                Id = newRegion.Id,
                Code = newRegion.Code,
                Name = newRegion.Name,
                RegionImageUrl = newRegion.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById),new { id= regionDto.Id }, regionDto);
        }

        // PUT to update region
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id, [FromBody]UpdateRegionReqDto updateRegionReqDto)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            // map DTO to domain model
            //var regionDomainModel = new Region()
            //{
            //    Code = updateRegionReqDto.Code,
            //    Name = updateRegionReqDto.Name,
            //    RegionImageUrl = updateRegionReqDto.RegionImageUrl
            //};
            var regionDomainModel = _mapper.Map<Region>(updateRegionReqDto);

            regionDomainModel = await _regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map domain model to DTO
            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        // DELETE region by id
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
           var region = await _regionRepository.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            // Map domain model to DTO
            //var regionDto = new RegionDto()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }
    }
}
