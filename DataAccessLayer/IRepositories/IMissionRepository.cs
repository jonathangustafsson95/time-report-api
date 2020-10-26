using CommonLibrary.Model;
using DataAccessLayer.IReppositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.IRepositories
{
    public interface IMissionRepository: IGenericRepository<Mission>
    {
        List<Mission> GetAllByCustomerId(int id);
    }
}

