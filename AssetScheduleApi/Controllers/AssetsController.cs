using AssetScheduleApi.Models.Entities;
using AssetScheduleApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssetScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {        
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;            
        }

        /// <summary>
        /// Retrieves all assets.
        /// </summary>
        /// <returns>A list of assets if found; otherwise, a NotFound response.</returns>
        /// <response code="200">Returns the list of assets.</response>
        /// <response code="404">If no assets are found.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            var assets = await _assetService.GetAllAssetsAsync();
            if (assets == null || !assets.Any())
            {
                return NotFound();
            }
            return Ok(assets);
        }

        /// <summary>
        /// Retrieves a specific asset by its ID.
        /// </summary>
        /// <param name="id">The ID of the asset to retrieve.</param>
        /// <returns>The asset if found; otherwise, a NotFound response.</returns>
        /// <response code="200">Returns the requested asset.</response>
        /// <response code="404">If the asset with the specified ID is not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(long id)
        {
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            return Ok(asset);
        }

        /// <summary>
        /// Updates a specific Asset identified by its ID.
        /// </summary>
        /// <param name="id">The ID of the asset to update.</param>
        /// <param name="assetUpdate">The asset data used for the update. Only the Name is updated.</param>
        /// <returns>
        /// An IActionResult that results in a 204 No Content response if the update is successful,
        /// a 404 Not Found response if the asset is not found, or a 400 Bad Request response
        /// if there is a mismatch between the asset ID and the ID provided in the route.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(long id, Asset assetUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _assetService.UpdateAssetAsync(id, assetUpdate);

            if (!updateResult)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new Asset.
        /// </summary>
        /// <param name="asset">The Asset object to create.</param>
        /// <returns>A 201 Created response with the newly created Asset.</returns>
        /// <response code="201">Asset created successfully.</response>
        /// <response code="400">If the asset is null or invalid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Asset))]
        public async Task<ActionResult<Asset>> PostAsset(Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Asset createdAsset = await _assetService.CreateAssetAsync(asset);
            return CreatedAtAction("GetAsset", new { id = createdAsset.Id }, createdAsset);
        }

        /// <summary>
        /// Deletes a specific Asset.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(long id)
        {
            var deleteResult = await _assetService.DeleteAssetAsync(id);
            if (!deleteResult)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
