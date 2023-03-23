using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities.Wrappers
{
    public class TransactionWithSupportAccountProductCard
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public string TransactionState { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime TransactionHour { get; set; }
        public string Description { get; set; }
        public decimal OldBalance { get; set; }
        public decimal FinalBalance { get; set; }
        public AccountsWithCustomers Accounts { get; set; }
        public Cards Cards { get; set; }
        public ProductsWithCustomers Products { get; set; }
        public SupportStaff SupportStaff { get; set; }
    }
}
