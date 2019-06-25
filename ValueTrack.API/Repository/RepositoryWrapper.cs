using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
        private IClientRepository _client;
        private IClientRegionRepository _clientRegion;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IOwnerRepository Owner
        {
            get
            {
                if(_owner == null)
                {
                    _owner = new OwnerRepository(_repoContext);
                }

                return _owner;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if(_account == null)
                {
                    _account = new AccountRepository(_repoContext);
                }

                return _account;
            }
        }

        public IClientRepository Client
        {
            get
            {
                if(_client == null)
                {
                    _client = new ClientRepository(_repoContext);
                }

                return _client;
            }
        }

        public IClientRegionRepository ClientRegion
        {
            get
            {
                if (_clientRegion == null)
                {
                    _clientRegion = new ClientRegionRepository(_repoContext);
                }

                return _clientRegion;
            }
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
