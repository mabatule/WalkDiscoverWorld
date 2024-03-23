using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);

    }
}
