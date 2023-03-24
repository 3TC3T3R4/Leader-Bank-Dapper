using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.Entities
{
    public class Transaction
    {
        public int Transaction_Id { get; set; }
        public int Id_Account { get; set; }       
        public string TransactionDate { get; set; }
        public string TransactionHour { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal OldBalance { get; set; }
        public decimal FinalBalance { get; set; }
        public string TransactionState { get; set; }

        public Transaction() { }

        public static void Validate(Transaction transaction)
        {
            if (transaction.Id_Account == null)
            {
                throw new ArgumentNullException("The Id Account type is required.");
            }
            if (transaction.TransactionDate == null)
            {
                throw new ArgumentNullException("The TransactionDate type is required.");
            }
            if (transaction.TransactionHour == null)
            {
                throw new ArgumentNullException("The TransactionHour type is required.");
            }
            if (transaction.TransactionType == null)
            {
                throw new ArgumentNullException("The TransactionType type is required.");
            }
            if (transaction.Description == null)
            {
                throw new ArgumentNullException("The Description phone is required.");
            }
            if (transaction.Amount == null)
            {
                throw new ArgumentNullException("The Amount type is required.");
            }
            if (transaction.OldBalance == null)
            {
                throw new ArgumentNullException("The OldBalance type is required.");
            }
            if (transaction.FinalBalance == null)
            {
                throw new ArgumentNullException("The FinalBalance is required.");
            }
            if (transaction.TransactionState == null)
            {
                throw new ArgumentNullException("The TransactionState is required.");
            }
        }

      
    }
}
