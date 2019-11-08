using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TddDemo.Web.Data;
using TddDemo.Web.Models;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace TddDemo.Test
{
    public class ForecastTests
    {
        [Fact]
        public void CreateForecastFromBudgetShouldSucessWhenBudgetExist()
        {
            // Arrenge
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TddDemoInMemory")
                .Options;

            var budget = new Budget
            {
                Name = "Test budget",
                BudgetDetails = new List<BudgetDetail>
                    {
                        new BudgetDetail
                        {
                            Description = "Super Market",
                            BudgetCost = 200, // B3
                            ToDateCost = 150, // C3
                        },
                        new BudgetDetail
                        {
                            Description = "Service Providers",
                            BudgetCost = 120, // B4
                            ToDateCost = 100, // C4
                        },
                        new BudgetDetail
                        {
                            Description = "Health",
                            BudgetCost = 50, // B5
                            ToDateCost = 30, // C5
                        },
                        new BudgetDetail
                        {
                            Description = "Gasoline",
                            BudgetCost = 100, // B6
                            ToDateCost = 50, // C6
                        },
                        new BudgetDetail
                        {
                            Description = "Debts",
                            BudgetCost = 130, // B7
                            ToDateCost = 130, // C7
                        },
                        new BudgetDetail
                        {
                            Description = "Fun & Entertainment",
                            BudgetCost = 50, // B8
                            ToDateCost = 50, // C8
                        }
                    }
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Budgets.Add(budget);
                context.SaveChanges();
            }

            var inputModel = new NewForecastInputModel
            {
                BudgetId = budget.Id,
                ForecastDate = DateTime.Now.Date
            };

            // Act
            Forecast forecast = null;
            using (var context = new ApplicationDbContext(options))
            {
                forecast = TheCode(context, inputModel);
            }

            // Assert
            forecast.Should().NotBeNull();
            forecast.Id.Should().NotBe(0);

            using (var context = new ApplicationDbContext(options))
            {
                var budgetOnDb = context.Budgets.Include(b => b.BudgetDetails).FirstOrDefault(b => b.Id == budget.Id);
                var forecastOnDb = context.Forecasts.Include(f => f.ForecastDetails).FirstOrDefault(f => f.Id == forecast.Id);

                forecastOnDb.Should().NotBeNull();
                forecastOnDb.BudgetId.Should().Be(budgetOnDb.Id);
                forecastOnDb.Date.Should().Be(inputModel.ForecastDate);

                forecastOnDb.ForecastDetails.Count.Should().Be(budgetOnDb.BudgetDetails.Count);
                foreach (var budgetDetail in budgetOnDb.BudgetDetails)
                {
                    var correspondingForecastDetail = forecastOnDb.ForecastDetails.FirstOrDefault(f => f.BudgetDetailId == budgetDetail.Id);
                    correspondingForecastDetail.Should().NotBeNull();

                    correspondingForecastDetail.BudgetCost.Should().Be(budgetDetail.BudgetCost);
                    correspondingForecastDetail.ForecastCost.Should().Be(budgetDetail.BudgetCost);
                    correspondingForecastDetail.ToDateCost.Should().Be(budgetDetail.ToDateCost);
                }
            }

        }

        private Forecast TheCode(ApplicationDbContext db, NewForecastInputModel inputModel)
        {
            var budget = db.Budgets.Include(b => b.BudgetDetails).FirstOrDefault(b => b.Id == inputModel.BudgetId);

            var newForecast = new Forecast
            {
                BudgetId = budget.Id,
                Date = inputModel.ForecastDate
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
            db.Forecasts.Add(newForecast);
            db.SaveChanges();
            return newForecast;
        }
    }
}
