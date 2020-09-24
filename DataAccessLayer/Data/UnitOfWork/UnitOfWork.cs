using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using CommonLibrary.Model;
using Data.Model;
using DataAccessLayer.Data;
using DataAccessLayer.Data.IRepositories;
using DataAccessLayer.Data.IReppositories;
using DataAccessLayer.Data.Repositories;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BulbasaurContext _context;
        public IGenericRepository<Customer> CustomerRepository { get; private set; }
        public IGenericRepository<FavoriteMission> FavoriteMissionRepository { get; private set; }
        public IGenericRepository<Mission> MissionRepository { get; private set; }
        public IGenericRepository<MissionMember> MissionMemberRepository { get; private set; }
        public IGenericRepository<Registry> RegistryRepository { get; private set; }
        public IGenericRepository<Task> TaskRepository { get; private set; }
        public IGenericRepository<User> UserRepository { get; private set; }

        public UnitOfWork(BulbasaurContext context)
        {
            this._context = context;
            CustomerRepository=new GenericRepository<Customer>(context);
            FavoriteMissionRepository= new GenericRepository<FavoriteMission>(context);
            MissionRepository=new GenericRepository<Mission>(context);
            MissionMemberRepository= new GenericRepository<MissionMember>(context);
            RegistryRepository=new GenericRepository<Registry>(context);
            TaskRepository=new GenericRepository<Task>(context);
            UserRepository=new GenericRepository<User>(context);
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
