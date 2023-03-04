using AssestManagement.Interfaces;
using AssestManagement.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Mvc;

namespace AssestManagement.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly string _ConnectionString;

        public AssetRepository(IConfiguration configuration)
        {
            _ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddAsset(Asset asset)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AssetName", asset.AssetName);
                    parameters.Add("@AssetCategory", asset.AssetCategory);
                    parameters.Add("@AssetType", asset.AssetType);
                    //   parameters.Add("@AssetId", DbType.Int32, direction: ParameterDirection.Output);

                    return await connection.ExecuteAsync("InsertAsset", parameters, commandType: CommandType.StoredProcedure);
                     

                  //  var assetId = await connection.QuerySingleAsync<int>("SELECT SCOPE_IDENTITY()");

                 // return assetid;
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("Error in AddAsset method", ex);
            }
        }

        public async Task<int>AddAssetImage(AssetImage assetImage)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {


                    var parameters = new DynamicParameters();
                    parameters.Add("@AssetId", assetImage.AssetId);
                    parameters.Add("@Name", assetImage.Name);
                    parameters.Add("@Content", assetImage.Content);
                     parameters.Add("@AssetImageId", DbType.Int32, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync("InsertAssetImage", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("@AssetImageId");
                }
            }
            catch (SqlException ex)
            { 
                throw new Exception("Error in AddAsset method", ex); 
            }
        }
 

       public async Task<int> AddDocument(Document document)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {


                    var parameters = new DynamicParameters();
                    parameters.Add("@AssetId", document.AssetId);
                    parameters.Add("@DocumentTitle", document.DocumentTitle);
                    parameters.Add("@Description", document.Description);
                    parameters.Add("@FileName", document.FileName);
                    parameters.Add("@FileExtension", document.FileExtension);
                    parameters.Add("@DocumentId", DbType.Int32, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync("InsertDocument", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("@DocumentId");
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error in AddAsset method", ex);
            }
        }
        public async Task<IEnumerable<Asset>> GetAssets()
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    return await connection.QueryAsync<Asset>("GetAssets", commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error in GetAssets method", ex);
            }
        }

        public async Task<Asset> GetAssetById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AssetId", id);

                    return await connection.QueryFirstOrDefaultAsync<Asset>("GetAssetById", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error in GetAssetById method", ex);
            }
        }
        public async Task UpdateAsset(Asset asset)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AssetId", asset.AssetId);
                    parameters.Add("@AssetName", asset.AssetName);
                    parameters.Add("@AssetCategory", asset.AssetCategory);
                    parameters.Add("@AssetType", asset.AssetType);

                    await connection.ExecuteAsync("UpdateAsset", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error in UpdateAsset method", ex);
            }
        }
        //public async Task <bool> BulkUpdateAssets(IEnumerable<int> assetIds, string assetName, string assetCategory, string assetType)
        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(_ConnectionString))
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@AssetIds",ParseCsvToIntList(assetIds));
        //            parameters.Add("@AssetName", assetName);
        //            parameters.Add("@AssetCategory", assetCategory);
        //            parameters.Add("@AssetType", assetType);

        //            var rowsAffected = await connection.ExecuteAsync("BulkUpdateAssets", parameters, commandType: CommandType.StoredProcedure);

        //            return rowsAffected > 0;
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new Exception("Error in BulkUpdateAssets method", ex);
        //    }
        //}
        //private IEnumerable<int> ParseCsvToIntList(string csv)
        //{
        //    var intList = new List<int>();
        //    if (!string.IsNullOrEmpty(csv))
        //    {
        //        foreach (var id in csv.Split(','))
        //        {
        //            if (int.TryParse(id.Trim(), out int intId))
        //            {
        //                intList.Add(intId);
        //            }
        //        }
        //    }
        //    return intList;
        //}
    }

}

   