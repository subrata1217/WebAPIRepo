using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IClientRegionRepository: IRepositoryBase<ClientRegion>
    {
        IEnumerable<ClientRegion> ClientRegionByClient(Guid clientId);
    }
}
