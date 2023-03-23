using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways;
using leader.bank.domain.usecases.Gateways.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.usecases.UseCases
{
    public class TransactionUseCase : ITransactionUseCase
    {


        private readonly ITransactionRepository _transactionRepository;

        public TransactionUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }


        public async Task<Transaction> AddTransaction(Transaction transaction) {


            return await _transactionRepository.CreateTransactionAsync(transaction);


        }

        public async Task<List<Transaction>> GetTransaction() {
            return await _transactionRepository.GetTransactionAsync();
        }






    }
}
