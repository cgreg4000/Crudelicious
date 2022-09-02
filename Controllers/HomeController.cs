using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Crudelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace Crudelicious.Controllers
{
    public class HomeController : Controller
    {
        private DishesContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DishesContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.AllDishes = _context.Dishes.OrderByDescending(c => c.CreatedAt).ToList();
            return View();
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult New(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet("details/{dishId}")]
        public IActionResult Details(int dishId)
        {
            ViewBag.OneDish= _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View();
        }

        [HttpGet("delete/{dishId}")]
        public IActionResult Delete(int dishId)
        {   
            Dish RetrievedDish = _context.Dishes.SingleOrDefault(d => d.DishId == dishId);
            _context.Dishes.Remove(RetrievedDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish DishToUpdate = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View(DishToUpdate);
        }
        
        [HttpPost("update/{dishId}")]
        public IActionResult UpdateDish(int dishId, Dish updatedDish)
        {
            if (ModelState.IsValid)
            {
                Dish OldDishInfo = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
                OldDishInfo.ChefName = updatedDish.ChefName;
                OldDishInfo.DishName = updatedDish.DishName;
                OldDishInfo.Calories = updatedDish.Calories;
                OldDishInfo.Tastiness = updatedDish.Tastiness;
                OldDishInfo.Description = updatedDish.Description;
                OldDishInfo.UpdatedAt = DateTime.Now;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("EditDish", updatedDish);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
