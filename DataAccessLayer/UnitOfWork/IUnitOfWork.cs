using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using CommonLibrary.Model;
using Data.Model;
using DataAccessLayer.IRepositories;
using DataAccessLayer.IReppositories;
using DataAccessLayer.Repositories;

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
