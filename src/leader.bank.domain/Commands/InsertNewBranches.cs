using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Commands
{
    public class InsertNewBranches
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
