﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Model;

namespace DataAccessLayer.Data.IRepositories
{
    public interface IFavoriteMissionRepository: IGenericRepository<FavoriteMission>
    {
        List<FavoriteMission> GetFavoriteMissionsById(int id);
    }
}
