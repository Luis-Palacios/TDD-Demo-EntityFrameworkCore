using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddDemo.Web.Data;
using TddDemo.Web.Models;

namespace TddDemo.Web.Controllers
{
    public class ForecastController: Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ForecastController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewForecastInputModel newForecastInputModel)
        {
            // Create forecast from budget and get id of the forecast that was created
            return RedirectToAction(nameof(Details), new { id = 1 });
        }

        public async Task<IActionResult> Details(int id)
        {
            // Get forecast from database do calculations and send data to the view
            ViewBag.ForecastId = id;
            return View();
        }
    }
}
