using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            var walkDomainModel = await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkByIdAsync(Guid id)
        {
            var walkToDelete = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkToDelete == null) return null;
            _dbContext.Walks.Remove(walkToDelete);
            await _dbContext.SaveChangesAsync();
            return walkToDelete;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var walk = await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null) return null;
            return walk;
        }

        public async Task<Walk?> UpdateWalkByIdAsync(Guid id, Walk walk)
        {
            var walkToUpdate = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkToUpdate == null) return null;
            walkToUpdate.Name = walk.Name;
            walkToUpdate.Description = walk.Description;
            walkToUpdate.LengthInKm = walk.LengthInKm;
            walkToUpdate.WalkImageUrl = walk.WalkImageUrl;
            walkToUpdate.DifficultyId = walk.DifficultyId;
            walkToUpdate.RegionId = walk.RegionId;
            await _dbContext.SaveChangesAsync();
            return walkToUpdate;
        }
    }
}
