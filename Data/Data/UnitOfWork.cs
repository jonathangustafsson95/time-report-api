using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;

namespace Data.Data
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DataContext _context;
        public IGenericRepository<Customer> CustomerRepository { get; private set; }
        public IGenericRepository<FavoriteMission> FavoriteMissionRepository { get; private set; }
        public IGenericRepository<Mission> MissionRepository { get; private set; }
        public IGenericRepository<MissionMember> MissionMemberRepository { get; private set; }
        public IGenericRepository<Registry> RegistryRepository { get; private set; }
        public IGenericRepository<Task> TaskRepository { get; private set; }
        public IGenericRepository<User> UserRepository { get; private set; }

        public UnitOfWork(DataContext context)
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
