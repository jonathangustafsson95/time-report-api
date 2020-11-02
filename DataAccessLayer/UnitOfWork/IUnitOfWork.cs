using System;
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IFavoriteMissionRepository FavoriteMissionRepository { get; }
        IMissionRepository MissionRepository { get; }
        IMissionMemberRepository MissionMemberRepository { get; }
        IRegistryRepository RegistryRepository { get; }
        ITaskRepository TaskRepository { get; }
        IUserRepository UserRepository { get; }
        void Save();
    }
}
