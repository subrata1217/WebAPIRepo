using Entities.Models;

namespace Entities.Extensions
{
    public static class ClientExtensions
    {
        public static void Map(this Client dbClient, Client client)
        {
            dbClient.Name = client.Name;
            dbClient.Currency_ID = client.Currency_ID;
            dbClient.PortfolioSize = client.PortfolioSize;
            dbClient.UnitOfSpace_ID = client.UnitOfSpace_ID;
            dbClient.Industry_ID = client.Industry_ID;
            dbClient.ContractType_ID = client.ContractType_ID;
        }
    }
}
