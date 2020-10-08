using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using CommonLibrary.Model;
using Data.Model;
using DataAccessLayer;
using DataAccessLayer.IRepositories;
using DataAccessLayer.IReppositories;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BulbasaurDevContext _context;
        public ICustomerRepository CustomerRepository { get; }
        public IFavoriteMissionRepository FavoriteMissionRepository { get; }
        public IMissionRepository MissionRepository { get; }
        public IMissionMemberRepository MissionMemberRepository { get; }
        public IRegistryRepository RegistryRepository { get; }
        public ITaskRepository TaskRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(BulbasaurDevContext context)
        {
            this._context = context;
            CustomerRepository = new CustomerRepository(context);
            FavoriteMissionRepository = new FavoriteMissionRepository(context);
            MissionRepository = new MissionRepository(context);
            MissionMemberRepository = new MissionMemberRepository(context);
            RegistryRepository = new RegistryRepository(context);
            TaskRepository = new TaskRepository(context);
            UserRepository = new UserRepository(context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
