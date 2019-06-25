using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class ClientDto: IEntity
    {
        public Guid Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public Guid? Currency_ID { get; set; }
        public int PortfolioSize { get; set; }
        public Guid? UnitOfSpace_ID { get; set; }
        public Guid? Industry_ID { get; set; }
        public int? ContractType_ID { get; set; }

        public IEnumerable<ClientRegion> ClientRegions { get; set; }

        public ClientDto()
        {

        }
        public ClientDto(Client client)
        {
            Id = client.Id;
            Abbreviation = client.Abbreviation;
            Name = client.Name;
            Currency_ID = client.Currency_ID;
            PortfolioSize = client.PortfolioSize;
            UnitOfSpace_ID = client.UnitOfSpace_ID;
            Industry_ID = client.Industry_ID;
            ContractType_ID = client.ContractType_ID;
        }

    }
}
