using AutoMapper;
using Dapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using leader.bank.infrastructure.Gateway;
using Microsoft.IdentityModel.Tokens;

namespace leader.bank.infrastructure.SqlAdapter
{
    public class CardRepository : ICardRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly IMapper _mapper;
        private readonly string _tableName = "Cards";

        public CardRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<InsertNewCard> CreateCardAsync(Card card)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            //verify that account exist
            var accountSql = $"SELECT COUNT(*) FROM Accounts WHERE Account_Id = @idAccount;";
            var accountCount = await connection.ExecuteScalarAsync<int>(accountSql, new { idAccount = card.Id_Account });

            if (accountCount == 0)
            {
                throw new Exception("The account doesn't exist.");
            }

            var cardToCreate = new Card
            {
                Id_Account = card.Id_Account,
                NumberCard = card.NumberCard,
                Cvc = card.Cvc,
                EmissionDate = DateTime.Now,
                ExpirationDate = null,
                CardState = card.CardState,
            };

            Card.Validate(cardToCreate);

            var sql = $"INSERT INTO {_tableName} (Id_Account, NumberCard, Cvc, EmissionDate, ExpirationDate, CardState) " +
                $"VALUES (@Id_Account, @NumberCard, @Cvc, @EmissionDate, @ExpirationDate, @CardState);";
            var result = await connection.ExecuteScalarAsync(sql, cardToCreate);
            connection.Close();
            return _mapper.Map<InsertNewCard>(cardToCreate);
        }

        public async Task<List<Card>> GetCardsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync ();

            var sql = $"SELECT * FROM {_tableName}";
            var result = await connection.QueryAsync<Card>(sql);
            if (result.IsNullOrEmpty())
            {
                throw new Exception("There aren't cards to show.");
            }
            connection.Close();
            return result.ToList();
        }
    }
}
