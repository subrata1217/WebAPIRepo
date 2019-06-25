namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        IClientRepository Client { get; }
        IClientRegionRepository ClientRegion { get; }
        void Save();
    }
}
