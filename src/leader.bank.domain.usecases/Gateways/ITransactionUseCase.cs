using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.usecases.Gateways
{
    public interface ITransactionUseCase
    {

        Task<InsertNewTransaction> AddTransaction(Transaction transaction);

        Task<List<Transaction>> GetTransaction();

        //Task<List<CustomerWithAccounts>>GetDoneTransactionById(int id);



    }
}
