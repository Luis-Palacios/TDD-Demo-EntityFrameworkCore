namespace TddDemo.Web.Data
{
    public class ForecastDetail
    {
        public int Id { get; set; }
        public int ForecastId { get; set; }
        public int BudgetDetailId { get; set; }
        public decimal ForecastCost { get; set; }
        public decimal BudgetCost { get; set; }
        public decimal ToDateCost { get; set; }
        
        public Forecast Forecast { get; set; }
        public BudgetDetail BudgetDetail { get; set; }
    }
}