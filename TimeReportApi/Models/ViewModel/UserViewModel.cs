using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class UserViewModel
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string eMail { get; set; }
        public UserViewModel ConvertToViewModel(User user)
        {
            return new UserViewModel { userId = user.UserId, password = user.Password, eMail = user.EMail, userName = user.UserName };
        }
    }
}
