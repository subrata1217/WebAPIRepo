﻿using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ClientRepository: RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public IEnumerable<Client> GetAllClients()
        {
            return FindAll()
                .Where(c => c.IsDeleted == false)
                .OrderBy(c => c.Name)
                .ToList();
        }

        public Client GetClientById(Guid clientId)
        {
            return FindByCondition(c => c.Id.Equals(clientId))
                .DefaultIfEmpty(new Client())
                .FirstOrDefault();
        }

        public ClientDto GetClientWithDetails(Guid clientId)
        {
            return new ClientDto(GetClientById(clientId))
            {
                ClientRegions = RepositoryContext.ClientRegions.Where(c => c.ClientId.Equals(clientId))
            };
        }

        public void CreateClient(Client client)
        {
            client.Id = Guid.NewGuid();
            Create(client);
        }

        public void UpdateClient(Client dbClient, Client client)
        {
            dbClient.Map(client);
            Update(dbClient);
        }

        public void DeleteClient(Client client)
        {
            Delete(client);
        }
    }
}
