using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddDemo.Web.Data;
using TddDemo.Web.Models;

namespace TddDemo.Web.Services
{
    public class ForecastService : IForecastService
    {
        private readonly ApplicationDbContext dbContext;

        public ForecastService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Forecast CreateForecastFromBudget(NewForecastInputModel newForecastInputModel)
        {
            var budget = dbContext.Budgets.Include(b => b.BudgetDetails).FirstOrDefault(b => b.Id == newForecastInputModel.BudgetId);

            var newForecast = new Forecast
            {
                BudgetId = budget.Id,
                Date = newForecastInputModel.ForecastDate
            };
            foreach (var budgetDetail in budget.BudgetDetails)
            {
                newForecast.ForecastDetails.Add(new ForecastDetail
                {
                    BudgetCost = budgetDetail.BudgetCost,
                    BudgetDetailId = budgetDetail.Id,
                    ForecastCost = budgetDetail.BudgetCost,
                    ToDateCost = budgetDetail.ToDateCost,
                });
            }
            dbContext.Forecasts.Add(newForecast);
            dbContext.SaveChanges();
            return newForecast;
        }
    }
}
