using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,
                        IMapper mapper)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        // GET all regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
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

            return Ok(regionsDto);
        }

        // GET region by id
        [HttpGet]
        [Route("{id:guid}")]
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
        public async Task<IActionResult> CreateRegion([FromBody]AddRegionReqDto region)
        {
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
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id, [FromBody]UpdateRegionReqDto updateRegionReqDto)
        {
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
