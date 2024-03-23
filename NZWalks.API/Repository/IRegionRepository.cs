using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region> DeleteAsync(Guid id);

        Task<Region> UpdateAsync(Guid id, Region region);
    }
}
