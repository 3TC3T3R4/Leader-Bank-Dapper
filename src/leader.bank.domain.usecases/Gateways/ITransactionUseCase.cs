using leader.bank.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.usecases.Gateways
{
    public interface ITransactionUseCase
    {

        Task<Transaction> AddTransaction(Transaction transaction);

        Task<List<Transaction>> GetTransaction();



    }
}
