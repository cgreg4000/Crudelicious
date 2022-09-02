using System;
using System.ComponentModel.DataAnnotations;
namespace Crudelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required(ErrorMessage = "Please enter the name of the chef.")]
        public string ChefName { get; set; }

        [Required(ErrorMessage = "Please enter the name of the dish.")]
        public string DishName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Please enter a number for calories that is greater than 0.")]
        public int Calories { get; set; }

        
        [Required]
        [Range(1,5, ErrorMessage = "Please give a tastiness rating between 1 to 5.")]
        public int Tastiness { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}