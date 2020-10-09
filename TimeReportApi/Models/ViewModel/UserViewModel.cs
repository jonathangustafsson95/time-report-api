using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public UserViewModel ConvertToViewModel(User user)
        {
            return new UserViewModel { UserId = user.UserId, Password = user.Password, EMail = user.EMail, UserName = user.UserName };
        }
    }
}
