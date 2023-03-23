using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities
{
    public class Accounts
    {
        public int Account_Id { get; set; }
        public int Id_Customer { get; set; }       
        public string AccountType { get; set; }       
        public decimal Balance { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }  
        public decimal ManagementCost { get; set; }
        public string AccountState { get; set; }

    }
}
