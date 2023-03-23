using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Commands
{
    public class InsertNewAccounts
    {
        public string AccountType { get; set; }
        public string AccountState { get; set; }
        public decimal Balance { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public decimal InterestRate { get; set; }
        public int Id_Customers { get; set; }
    }
}
