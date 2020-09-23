using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;

namespace Data.Data
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<FavoriteMission> FavoriteMissionRepository { get; }
        IGenericRepository<Mission> MissionRepository { get; }
        IGenericRepository<MissionMember> MissionMemberRepository { get; }
        IGenericRepository<Registry> RegistryRepository { get; }
        IGenericRepository<Task> TaskRepository { get; }
        IGenericRepository<User> UserRepository { get; }

    }

    }
