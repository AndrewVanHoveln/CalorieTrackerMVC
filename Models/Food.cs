using System.ComponentModel.DataAnnotations;
namespace Calorie_Tracking_App.Models;

public class Food
{
    public int Id { get; set; }

    [Required]
    public double Carbs { get; set; }

    [Required]
    public double Protien { get; set; }

    [Required]
    public double Fats { get; set; }

    [Required]
    public double Calories { get; set; }
    
    public string? Name { get; set; }
}