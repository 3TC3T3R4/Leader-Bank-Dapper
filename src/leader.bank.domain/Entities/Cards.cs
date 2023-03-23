using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities
{
    public class Cards
    {
        public int Card_Id { get; set; }
        public int Id_Account { get; set; }
        public string NumberCard { get; set; }
        public string Cvc { get; set; }
        public DateTime EmissionDate { get; set; }       
        public DateTime ExpirationDate { get; set; }
        public string CardState { get; set; }
    }
}
