using System.Data;

namespace leader.bank.infrastructure.Gateway
{
    public interface IDbConnectionBuilder
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}
