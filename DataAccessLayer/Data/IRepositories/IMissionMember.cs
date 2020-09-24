using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer.Data.IReppositories;


namespace DataAccessLayer.Data.IRepositories
{
    public interface IMissionMember: IGenericRepository<MissionMember>
    {
        List<MissionMember> GetAllByUserId(int id);
    }
}
