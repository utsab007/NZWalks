using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET all regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from database - Regions domain table
            var regions = _dbContext.Regions.ToList();

            var regionsDto = new List<RegionDto>();
            // Map domain model to DTO
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            };

            return Ok(regionsDto);
        }

        // GET region by id
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetRegionById([FromRoute]Guid id)
        {
            // var region = _dbContext.Regions.Find(id);
            // Get Region model from database
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            // Map domain model to DTO
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // POST to create new region
        [HttpPost]
        public IActionResult CreateRegion([FromBody]AddRegionReqDto region)
        {
            // Map DTO to domain model
            var newRegion = new Region()
            {
                Id = Guid.NewGuid(),
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            _dbContext.Regions.Add(newRegion);
            _dbContext.SaveChanges();

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
        public IActionResult UpdateRegion([FromRoute]Guid id, [FromBody]UpdateRegionReqDto updateRegionReqDto)
        {
            // Get Region model from database
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            region.Code = updateRegionReqDto.Code;
            region.Name = updateRegionReqDto.Name;
            region.RegionImageUrl = updateRegionReqDto.RegionImageUrl;

            _dbContext.SaveChanges();

            // Map domain model to DTO
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // DELETE region by id
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            // Get Region model from database
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            _dbContext.Regions.Remove(region);
            _dbContext.SaveChanges();

            // Map domain model to DTO
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
