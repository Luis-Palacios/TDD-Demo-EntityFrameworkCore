using System;
using System.Collections.Generic;

namespace TddDemo.Web.Data
{
    public class Forecast
    {
        public Forecast()
        {
            ForecastDetails = new HashSet<ForecastDetail>();
        }

        public int Id { get; set; }
        public int BudgetId { get; set; }
        public DateTime Date { get; set; }

        public Budget Budget { get; set; }

        public ICollection<ForecastDetail> ForecastDetails { get; set;}
    }
}
