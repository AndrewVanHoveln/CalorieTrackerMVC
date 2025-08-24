using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Calorie_Tracking_App.Data;
using Calorie_Tracking_App.Models;

namespace Calorie_Tracking_App.Controllers;

public class CalorieTrackerController : Controller
{
    private readonly ILogger<CalorieTrackerController> _logger;
    private readonly CalorieTrackingContext _context;

    public CalorieTrackerController(ILogger<CalorieTrackerController> logger, CalorieTrackingContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var entries = await _context.CalorieEntries.ToListAsync();
        return View(entries);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Food food)
    {
        if (!ModelState.IsValid)
        {
            _logger.Log(LogLevel.Information, "Add Model State is invalid");
            return RedirectToAction(nameof(Index));
        }
        await _context.AddAsync(food);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var food = await _context.FindAsync<Food>(id);
        if (food != null)
        {
            _context.Remove(food);
            await _context.SaveChangesAsync();
        }
        else
        {
            _logger.LogInformation($"food with id:{id} not found");
        }
        
        return RedirectToAction(nameof(Index));
    }
}
