using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities.Wrappers
{
   public class AccountsWithCustomers
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public string AccountState { get; set; }
        public decimal Balance { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public decimal InterestRate { get; set; }
        public Customers Customers { get; set; }



    }
}
