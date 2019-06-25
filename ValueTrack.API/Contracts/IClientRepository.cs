using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        IEnumerable<Client> GetAllClients();
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Client GetClientById(Guid clientId);
        Task<Client> GetClientByIdAsync(Guid clientId);
        ClientDto GetClientWithDetails(Guid clientId);
        Task<ClientDto> GetClientWithDetailsAsync(Guid clientId);
        void CreateClient(Client client);
        Task CreateClientAsyc(Client client);
        void UpdateClient(Client dbClient, Client client);
        Task UpdateClientAsync(Client dbClient, Client client);
        void DeleteClient(Client client);
        Task DeleteClientAsync(Client client);
    }
}
