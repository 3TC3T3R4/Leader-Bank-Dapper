using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities
{
    public class Transaction
    {
        public int Transaction_Id { get; set; }
        public int Id_Account { get; set; }       
        public string TransactionDate { get; set; }
        public string TransactionHour { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal OldBalance { get; set; }
        public decimal FinalBalance { get; set; }
        public string TransactionState { get; set; }
       
    }
}
