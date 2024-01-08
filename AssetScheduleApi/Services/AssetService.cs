using AssetScheduleApi.Models;
using AssetScheduleApi.Models.DTOs;
using AssetScheduleApi.Models.Entities;
using AssetScheduleApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AssetScheduleApi.Services
{
    public class AssetService : IAssetService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AssetService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AssetOutput> CreateAssetAsync(AssetInput assetInput)
        {
            // Map AssetInput to Asset entity
            Asset newAsset = _mapper.Map<Asset>(assetInput);

            // Add the new asset to the context and save changes
            _context.Assets.Add(newAsset);
            await _context.SaveChangesAsync();

            // Map the saved Asset entity back to AssetOutput
            AssetOutput assetOutput = _mapper.Map<AssetOutput>(newAsset);

            return assetOutput;
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
