using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetScheduleApi.Models;
using AssetScheduleApi.Models.Entities;

namespace AssetScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
          if (_context.Assets == null)
          {
              return NotFound();
          }
            return await _context.Assets.ToListAsync();
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(long id)
        {
          if (_context.Assets == null)
          {
              return NotFound();
          }
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
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
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            // Use the provided method to update the name
            if (assetUpdate.Name != null)
            {
                asset.Update(assetUpdate.Name);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AssetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Assets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset(Asset asset)
        {
          if (_context.Assets == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Assets'  is null.");
          }
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsset", new { id = asset.Id }, asset);
        }

        /// <summary>
        /// Deletes a specific Asset.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(long id)
        {
            if (_context.Assets == null)
            {
                return NotFound();
            }
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> AssetExists(long id)
        {
            return await _context.Assets.AnyAsync(e => e.Id == id);
        }
    }
}
