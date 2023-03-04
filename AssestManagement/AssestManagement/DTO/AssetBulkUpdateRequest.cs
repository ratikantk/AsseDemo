namespace AssestManagement.DTO
{
    public class AssetBulkUpdateRequest
    {
        public IEnumerable<int> AssetIds { get; set; }
        public string AssetName { get; set; }
        public string AssetCategory { get; set; }
        public string AssetType { get; set; }

    //    public IEnumerable<int>GetAssetIdsAsIntList()
    //    {
    //        return AssetIds.Split(',').Select(int.Parse);
    //    }
    }
}
