using AssetScheduleApi.Models.Entities;

namespace AssetScheduleApi.Services.Interfaces
{
    public interface IAssetService
    {
        Task<Asset> CreateAssetAsync(Asset asset);
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset?> GetAssetByIdAsync(long id);
        Task<bool> UpdateAssetAsync(long id, Asset assetUpdate);
        Task<bool> DeleteAssetAsync(long id);
    }
}
