using tunenest.Domain.Entities.Songs;

namespace tunenest.Domain.Entities.Users
{
    public class UserFollowSong
    {
        public UserFollowSong()
        {
        }

        public Guid user_id { get; set; }
        public long song_id { get; set; }

        public virtual Song song_Songs { get; set; }
        public virtual User user_Users { get; set; }
    }
}
