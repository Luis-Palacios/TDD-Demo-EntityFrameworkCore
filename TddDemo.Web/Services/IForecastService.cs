using TddDemo.Web.Data;
using TddDemo.Web.Models;

namespace TddDemo.Web.Services
{
    public interface IForecastService
    {
        Forecast CreateForecastFromBudget(NewForecastInputModel newForecastInputModel);
    }
}