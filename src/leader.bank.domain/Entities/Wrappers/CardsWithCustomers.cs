using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities.Wrappers
{
    public class CardsWithCustomers
    {
        public int Id { get; set; }
        public string CardType { get; set; }
        public string CardState { get; set; }
        public decimal Quota { get; set; }
        public DateTime EmissionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal CreditLimit { get; set; }
        public Customers Customers { get; set; }




    }
}
