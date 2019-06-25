using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ClientRegionRepository: RepositoryBase<ClientRegion>, IClientRegionRepository
    {
        public ClientRegionRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {

        }

        public IEnumerable<ClientRegion> ClientRegionByClient(Guid clientId)
        {
            return FindByCondition(r => r.ClientId.Equals(clientId)).ToList();
        }
    }
}
