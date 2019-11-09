using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddDemo.Web.Data;
using TddDemo.Web.Models;
using TddDemo.Web.Services;

namespace TddDemo.Web.Controllers
{
    public class ForecastController: Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IForecastService forecastService;

        public ForecastController(ApplicationDbContext dbContext, IForecastService forecastService)
        {
            this.dbContext = dbContext;
            this.forecastService = forecastService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewForecastInputModel newForecastInputModel)
        {
            var forecast = forecastService.CreateForecastFromBudget(newForecastInputModel);
            return RedirectToAction(nameof(Details), new { id = forecast.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            // Get forecast from database do calculations and send data to the view
            ViewBag.ForecastId = id;
            return View();
        }
    }
}
