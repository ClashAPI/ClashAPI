namespace backend.Models
{
    public class ClanFollow
    {
        public int Id { get; set; }
        public string ClanTag { get; set; }
        public string ClanName { get; set; }
        public virtual User Follower { get; set; }
    }
}