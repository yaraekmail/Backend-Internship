using System.Text.Json;
using CardiacPatientMonitoring.Api.Data;
using CardiacPatientMonitoring.Api.DTOs;
using CardiacPatientMonitoring.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CardiacPatientMonitoring.Api.Controllers;

// Handles medication catalog operations and Redis caching.
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class MedicationCatalogController : ControllerBase
{
    private readonly CardiacPatientMonitoringDbContext _context;
    private readonly IDistributedCache _cache;

    public MedicationCatalogController(
        CardiacPatientMonitoringDbContext context,
        IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }
    // Returns the medication catalog using cache-aside.
    [HttpGet]
    public async Task<IActionResult> GetCatalog()
    {
        const string cacheKey = "medication-catalog:all";

        // Check Redis first.
        var cachedCatalog = await _cache.GetStringAsync(cacheKey);

        if (cachedCatalog is not null)
        {
            return Ok(JsonSerializer.Deserialize<List<MedicationCatalogItem>>(cachedCatalog));
        }

        // Cache miss: load the catalog from SQL Server.
        var catalog = await _context.MedicationCatalogItems
            .AsNoTracking()
            .ToListAsync();

        // Store the catalog in Redis for 10 minutes.
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        };

        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(catalog),
            cacheOptions);

        return Ok(catalog);
    }
    // Creates a new medication catalog item and invalidates the cache.
    [HttpPost]
    public async Task<IActionResult> CreateCatalogItem(
        CreateMedicationCatalogItemRequest request)
    {
        var catalogItem = new MedicationCatalogItem
        {
            Name = request.Name,
            UnitPrice = request.UnitPrice,
            StockQuantity = request.StockQuantity
        };

        _context.MedicationCatalogItems.Add(catalogItem);

        await _context.SaveChangesAsync();

        // Invalidate the cached catalog after the write.
        await _cache.RemoveAsync("medication-catalog:all");

        return CreatedAtAction(
            nameof(GetCatalog),
            new { id = catalogItem.Id },
            catalogItem);
    }
    // Updates a medication catalog item and invalidates the cache.
[HttpPut("{id:int}")]
public async Task<IActionResult> UpdateCatalogItem(
    int id,
    UpdateMedicationCatalogItemRequest request)
{
    var catalogItem = await _context.MedicationCatalogItems
        .FirstOrDefaultAsync(item => item.Id == id);

    if (catalogItem is null)
    {
        return NotFound();
    }

    // Check that the item was not changed by another request.
    if (!catalogItem.RowVersion.SequenceEqual(request.RowVersion))
    {
        return Conflict("The medication catalog item was changed by another request.");
    }

    catalogItem.Name = request.Name;
    catalogItem.UnitPrice = request.UnitPrice;
    catalogItem.StockQuantity = request.StockQuantity;

    await _context.SaveChangesAsync();

    // Invalidate the cached catalog after the write.
    await _cache.RemoveAsync("medication-catalog:all");

    return Ok(catalogItem);
}
// Deletes a medication catalog item and invalidates the cache.
[HttpDelete("{id:int}")]
public async Task<IActionResult> DeleteCatalogItem(int id)
{
    var catalogItem = await _context.MedicationCatalogItems
        .FirstOrDefaultAsync(item => item.Id == id);

    if (catalogItem is null)
    {
        return NotFound();
    }

    _context.MedicationCatalogItems.Remove(catalogItem);

    await _context.SaveChangesAsync();

    // Invalidate the cached catalog after the write.
    await _cache.RemoveAsync("medication-catalog:all");

    return NoContent();
}
}
