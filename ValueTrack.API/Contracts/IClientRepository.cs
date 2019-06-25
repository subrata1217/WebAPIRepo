using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IClientRepository: IRepositoryBase<Client>
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(Guid clientId);
        ClientDto GetClientWithDetails(Guid clientId);
        void CreateClient(Client client);
        void UpdateClient(Client dbClient, Client client);
        void DeleteClient(Client client);
    }
}
