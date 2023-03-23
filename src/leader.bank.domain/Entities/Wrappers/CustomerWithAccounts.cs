namespace leader.bank.domain.Entities.Wrappers
{
    public class CustomerWithAccounts
    {
        public int Customer_id { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public string Occupation { get; set; }
        public string Gender { get; set; }
        public List<AccountWithCardAndTransactions> Accounts { get; set; } 
    }
}
