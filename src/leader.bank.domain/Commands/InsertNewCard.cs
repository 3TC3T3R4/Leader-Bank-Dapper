namespace leader.bank.domain.Commands
{
    public class InsertNewCard
    {
        public int Id_Account { get; set; }
        public string NumberCard { get; set; }
        public string Cvc { get; set; }
        public string CardState { get; set; }
    }
}
