using Microsoft.EntityFrameworkCore;
using Calorie_Tracking_App.Models;

namespace Calorie_Tracking_App.Data;
public class CalorieTrackingContext : DbContext
{
    public CalorieTrackingContext(DbContextOptions<CalorieTrackingContext> options) : base(options) { }
    public DbSet<Food> CalorieEntries { get; set; }
}