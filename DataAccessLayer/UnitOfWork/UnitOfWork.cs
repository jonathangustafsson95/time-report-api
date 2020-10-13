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
    public class UnitOfWork : IUnitOfWork
    {
        private ICustomerRepository _customerRepository;
        private IFavoriteMissionRepository _favoriteMissionRepository;
        private IMissionRepository _missionRepository;
        private IMissionMemberRepository _missionMemberRepository;
        private IRegistryRepository _registryRepository;
        private ITaskRepository _taskRepository;
        private IUserRepository _userRepository;

        private readonly BulbasaurDevContext _context;

        public UnitOfWork(BulbasaurDevContext context)
        {
            this._context = context;
        }

        public ICustomerRepository CustomerRepository 
        {
            get 
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_context);

                return _customerRepository;
            } 
        }
        public IFavoriteMissionRepository FavoriteMissionRepository
        {
            get
            {
                if (_favoriteMissionRepository == null)
                    _favoriteMissionRepository = new FavoriteMissionRepository(_context);

                return _favoriteMissionRepository;
            }
        }
        public IMissionRepository MissionRepository
        {
            get
            {
                if (_missionRepository == null)
                    _missionRepository = new MissionRepository(_context);

                return _missionRepository;
            }
        }
        public IMissionMemberRepository MissionMemberRepository
        {
            get
            {
                if (_missionMemberRepository == null)
                    _missionMemberRepository = new MissionMemberRepository(_context);

                return _missionMemberRepository;
            }
        }
        public IRegistryRepository RegistryRepository
        {
            get
            {
                if (_registryRepository == null)
                    _registryRepository = new RegistryRepository(_context);

                return _registryRepository;
            }
        }
        public ITaskRepository TaskRepository
        {
            get
            {
                if (_taskRepository == null)
                    _taskRepository = new TaskRepository(_context);

                return _taskRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);

                return _userRepository;
            }
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
