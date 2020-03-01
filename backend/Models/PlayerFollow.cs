namespace backend.Models
{
    public class PlayerFollow
    {
        public int Id { get; set; }
        public string PlayerTag { get; set; }
        public string PlayerName { get; set; }
        public virtual User Follower { get; set; }
    }
}