using leader.bank.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.usecases.Gateways.Repositories
{
    public interface ITransactionRepository
    {


        Task<Transaction> CreateTransactionAsync(Transaction transaction);

        Task<List<Transaction>> GetTransactionAsync();



    }
}
