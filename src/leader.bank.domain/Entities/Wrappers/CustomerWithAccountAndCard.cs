using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities.Wrappers
{
    public class CustomerWithAccountAndCard
    {
        public int Customer_Id { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public string Occupation { get; set; }
        public string Gender { get; set; }

        public List<AccountWithCardOnly> Accounts { get; set; } = new();
    }
}
