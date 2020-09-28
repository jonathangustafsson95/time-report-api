using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using CommonLibrary.Model;
using Data.Model;
using DataAccessLayer.IRepositories;
using DataAccessLayer.IReppositories;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Customer> CustomerRepository { get; }
        IFavoriteMissionRepository FavoriteMissionRepository { get; }
        IGenericRepository<Mission> MissionRepository { get; }
        IMissionMember MissionMemberRepository { get; }
        IRegistryRepository RegistryRepository { get; }
        IGenericRepository<Task> TaskRepository { get; }
        IGenericRepository<User> UserRepository { get; }

    }

    }
