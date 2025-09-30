using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Calorie_Tracking_App.Data;
using Calorie_Tracking_App.Models;
using Microsoft.AspNetCore.Authorization;

namespace Calorie_Tracking_App.Controllers;
[Authorize]
public class CalorieTrackerController : Controller
{
    private readonly ILogger<CalorieTrackerController> _logger;
    private readonly CalorieTrackingContext _context;

    public CalorieTrackerController(ILogger<CalorieTrackerController> logger, CalorieTrackingContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index(DateTime? Date = null)
    {
        if (Date == null) Date = DateTime.Today;
        ViewData["Date"] = Date;
        var entries = await _context.CalorieEntries.Where(
            (food) => food.LoggedAt.Date == Date.Value.Date)
            .ToListAsync();
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
        return RedirectToAction(nameof(Index), new {Date = food.LoggedAt});
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
        
        return RedirectToAction(nameof(Index), new {Date = food != null ? food.LoggedAt : DateTime.Today});
    }
}
