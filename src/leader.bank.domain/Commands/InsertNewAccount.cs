namespace leader.bank.domain.Commands
{
    public class InsertNewAccount
    {
        public int Id_Customer { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public DateTime OpenDate { get; set; }
        public decimal ManagementCost { get; set; }
        public string AccountState { get; set; }
    }
}
