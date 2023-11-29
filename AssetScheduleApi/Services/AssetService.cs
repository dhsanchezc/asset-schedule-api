using AssetScheduleApi.Models;
using AssetScheduleApi.Models.Entities;
using AssetScheduleApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetScheduleApi.Services
{
    public class AssetService : IAssetService
    {
        private readonly ApplicationDbContext _context;

        public AssetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }
        
        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            if (_context.Assets == null) return new List<Asset>();
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset?> GetAssetByIdAsync(long id)
        {
            if (_context.Assets == null) return null;
            return await _context.Assets.FindAsync(id);
        }

        public async Task<bool> UpdateAssetAsync(long id, Asset assetUpdate)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return false;
            }

            asset.Update(assetUpdate.Name!);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                if (!await AssetExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAssetAsync(long id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return false;
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> AssetExists(long id)
        {
            return await _context.Assets.AnyAsync(e  => e.Id == id);
        }
    }
}
