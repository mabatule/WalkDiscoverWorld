using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        public RegionsController(NZWalksDbContext dbContext, IMapper mapper, IRegionRepository regionRepository)
        {
            this.mapper = mapper;
            this.regionRepository = regionRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from Database
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Models to DTOs
            var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);

            //Return DTOs
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            // Get Region Domain Model From Database
            var regionDomain = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionDTO>>(regionDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            await regionRepository.CreateAsync(regionDomainModel);
            // Map Domain model back to DTO
            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
                return NotFound();

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }

        [HttpDelete]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteRegionDomainModel = await regionRepository.DeleteAsync(id);
            if (deleteRegionDomainModel == null)
                return NotFound();

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(deleteRegionDomainModel));
        }
    }
}
