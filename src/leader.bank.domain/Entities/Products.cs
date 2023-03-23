using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public string ProductState { get; set; }
        public string Descripction { get; set; }
        public decimal Amount { get; set; }
        public DateTime DeadlineDate { get; set; }
        public decimal InterestRate { get; set; }
        public int Id_Customers { get; set; }
        
    }
}

