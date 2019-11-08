using System.Collections.Generic;

namespace TddDemo.Web.Data
{
    public class Budget
    {
        public Budget()
        {
            BudgetDetails = new HashSet<BudgetDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BudgetDetail> BudgetDetails { get; set; }
    }
}
