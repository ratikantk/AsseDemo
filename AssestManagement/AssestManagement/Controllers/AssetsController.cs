using AssestManagement.DTO;
using AssestManagement.Interfaces;
using AssestManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssestManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IAssetRepository assetRepository, ILogger<AssetsController> logger)
        {
            _assetRepository = assetRepository;
            _logger = logger;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            try
            {
                var assets = await _assetRepository.GetAssets();
                return Ok(assets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting assets: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            try
            {
                var asset = await _assetRepository.GetAssetById(id);

                if (asset == null)
                {
                    return NotFound();
                }

                return Ok(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting asset with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Assets
        [HttpPost]
        public async Task<ActionResult<int>> AddAsset(Asset asset/*, AssetImage assetImage, Document document*/)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int assetId = await _assetRepository.AddAsset(asset);
                //  var a = assetId;
                //var assetImageId= await _assetRepository.AddAssetImage(assetImage);
                //var documentId = await _assetRepository.AddDocument(document);
                // asset.AssetId = assetId;
                //assetImage.AssetImageId = assetImageId;
                //document.DocumentId = documentId;
                //  return CreatedAtAction(nameof(GetAsset), new { id = asset.AssetId }, asset);
                return Ok("Asset Details Added");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding asset: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Assets/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsset( Asset asset)
        {
            try
            {
                
                await _assetRepository.UpdateAsset(asset);
                return Ok("Asset Details Added");


              //  return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating asset: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        // POST: api/Assets/BulkUpdate
        //[HttpPost("BulkUpdate")]
        //public async Task<IActionResult> BulkUpdateAssets([FromBody] AssetBulkUpdateRequest request)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var result = await _assetRepository.BulkUpdateAssets(request.AssetIds, request.AssetName, request.AssetCategory, request.AssetType);

        //        if (!result)
        //        {
        //            return NotFound();
        //        }

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error updating assets: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
    }
}
