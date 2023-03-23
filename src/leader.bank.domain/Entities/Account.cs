namespace leader.bank.domain.Entities
{
    public class Account
    {
        public int Account_Id { get; set; }
        public int Id_Customer { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal ManagementCost { get; set; }
        public string AccountState { get; set; }

        public Account() { }

        public static void Validate(Account account)
        {
            if (account.Id_Customer == 0 || account.Id_Customer == null)
            {
                throw new Exception("The customer doesn't exist.");
            }
            if (account.AccountType == null)
            {
                throw new ArgumentNullException("The account type is required.");
            }
            if (account.Balance < 0)
            {
                throw new ArgumentException("The balance cannot be less than zero.");
            }
            if (account.OpenDate == null)
            {
                throw new ArgumentNullException("The open date is required.");
            }
            if (account.ManagementCost < 0)
            {
                throw new ArgumentException("The management cost cannot be less than zero.");
            }
            if (account.AccountState == null)
            {
                throw new ArgumentNullException("The account state is required.");
            }
        }
    }
}
