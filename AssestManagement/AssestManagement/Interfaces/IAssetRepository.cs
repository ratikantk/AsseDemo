using AssestManagement.Models;

namespace AssestManagement.Interfaces
{
    public interface IAssetRepository
    {
       Task<int> AddAsset(Asset asset);
        Task UpdateAsset(Asset asset);
        Task<Asset> GetAssetById(int id);
        Task<IEnumerable<Asset>> GetAssets();
        //Task<bool> BulkUpdateAssets(IEnumerable<int> assetIds,string assetName, string assetCategory,string assetType);

        Task <int>AddAssetImage(AssetImage assetImage);
        Task<int> AddDocument(Document document);

    }
}
