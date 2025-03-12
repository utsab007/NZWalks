using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        // Create a new walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkReqDto addWalkReqDto)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            // Map DTO to domain model
            var walkDomainModel = _mapper.Map<Walk>(addWalkReqDto);
            var walk = await _walkRepository.CreateWalkAsync(walkDomainModel);
            // Map domain model to DTO
            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        // Get all walks
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await _walkRepository.GetAllWalksAsync();
            // Map domain model to DTO
            var walksDto = _mapper.Map<List<WalkDto>>(walks);
            return Ok(walksDto);
        }

        // Get walk by Id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walk = await _walkRepository.GetWalkByIdAsync(id);
            if(walk == null) return NotFound();
            // Map domain model to DTO
            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        // Update walk by Id
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalkById([FromRoute] Guid id, [FromBody] UpdateWalkReqDto updateWalkReqDto)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            // Map DTO to domain model
            var walkDomainModel = _mapper.Map<Walk>(updateWalkReqDto);
            walkDomainModel = await _walkRepository.UpdateWalkByIdAsync(id,walkDomainModel);
            if (walkDomainModel == null) return NotFound();
            // Map domain model to DTO
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        // Delete walk by Id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkById([FromRoute] Guid id)
        {
            var walk = await _walkRepository.DeleteWalkByIdAsync(id);
            if (walk == null) return NotFound();
            // Map domain model to DTO
            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

    }
}
