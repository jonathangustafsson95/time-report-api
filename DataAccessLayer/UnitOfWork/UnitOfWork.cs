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
        public IGenericRepository<Customer> CustomerRepository { get; }
        public IFavoriteMissionRepository FavoriteMissionRepository { get; }
        public IGenericRepository<Mission> MissionRepository { get; }
        public IMissionMember MissionMemberRepository { get; }
        public IRegistryRepository RegistryRepository { get; }
        public IGenericRepository<Task> TaskRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(BulbasaurDevContext context)
        {
            this._context = context;
            CustomerRepository = new GenericRepository<Customer>(context);
            FavoriteMissionRepository = new FavoriteMissionRepository(context);
            MissionRepository = new GenericRepository<Mission>(context);
            MissionMemberRepository = new MissionMemberRepository(context);
            RegistryRepository = new RegistryRepository(context);
            TaskRepository = new GenericRepository<Task>(context);
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
