using AssetScheduleApi.Models.DTOs;
using AssetScheduleApi.Models.Entities;

namespace AssetScheduleApi.Services.Interfaces
{
    public interface IAssetService
    {
        Task<AssetOutput> CreateAssetAsync(AssetInput assetInput);
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset?> GetAssetByIdAsync(long id);
        Task<bool> UpdateAssetAsync(long id, Asset assetUpdate);
        Task<bool> DeleteAssetAsync(long id);
    }
}
