namespace CommonLibrary.Model
{
    public class MissionMember
    {
        public int UserId { get; set; }
        public int MissionId { get; set; }
        public virtual User User { get; set; }
        public virtual Mission Mission { get; set; }
    }
}
