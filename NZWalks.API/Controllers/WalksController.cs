using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //CREATE Walk
        //POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                /// Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);

                // Map Domain model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            return BadRequest();
        }

        // GET Walks
        // GET /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // GET Walks By Id
        // GET /api/Walks/{Id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Update Walk By Id
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequest)
        {
            if (ModelState.IsValid)
            {
                var walksDomainModel = mapper.Map<Walk>(updateWalkRequest);

                walksDomainModel = await walkRepository.UpdateAsync(id, walksDomainModel);
                if (walksDomainModel == null)
                    return NotFound();
                // Map Domain Model to DTO
                return Ok(mapper.Map<WalkDto>(walksDomainModel));
            }
            return BadRequest();
        }

        // Delete Walks By Id
        // Delete /api/Walks/{Id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalksDomainModel = await walkRepository.DeleteAsync(id);
            if (deleteWalksDomainModel == null)
                return NotFound();

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(deleteWalksDomainModel));
        }
    }
}
