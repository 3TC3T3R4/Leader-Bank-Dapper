using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Commands
{
    public class InsertNewTransactions
    {
        public string TransactionType { get; set; }
        public string TransactionState { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime TransactionHour { get; set; }
        public string Description { get; set; }
        public decimal OldBalance { get; set; }
        public decimal FinalBalance { get; set; }
        public int Id_Accounts { get; set; }
        public int Id_Cards { get; set; }
        public int Id_Products { get; set; }
        public int Id_SupportStaff { get; set; }
    }
}
