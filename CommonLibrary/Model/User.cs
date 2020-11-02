using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CommonLibrary.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public ICollection<MissionMember> MissionMemberships { get; set; }
        public ICollection<FavoriteMission> MissionFavorites { get; set; }
    }
}
