namespace TddDemo.Web.Data
{
    public class BudgetDetail
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public string Description { get; set; }
        public decimal BudgetCost { get; set; }
        public decimal ToDateCost { get; set; }

        public Budget Budget { get; set; }
    }
}
