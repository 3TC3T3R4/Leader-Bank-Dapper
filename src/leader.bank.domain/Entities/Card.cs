namespace leader.bank.domain.Entities
{
    public class Card
    {
        public int Card_Id { get; set; }
        public int Id_Account { get; set; }
        public string NumberCard { get; set; }
        public string Cvc { get; set; }
        public DateTime EmissionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string CardState { get; set; }

        public Card() { }
        public static void Validate(Card card)
        {
            if (card.Id_Account == 0 || card.Id_Account == null)
            {
                throw new ArgumentException("The account doesn't exist.");
            }
            if (card.NumberCard == null)
            {
                throw new ArgumentNullException("The card number is required.");
            }
            if (card.Cvc == null)
            {
                throw new ArgumentNullException("The CVC is required.");
            }
            if (card.EmissionDate == null)
            {
                throw new ArgumentNullException("The emission date is required.");
            }
            if (card.CardState == null)
            {
                throw new ArgumentNullException("The card state is required.");
            }
        }
    }
}
