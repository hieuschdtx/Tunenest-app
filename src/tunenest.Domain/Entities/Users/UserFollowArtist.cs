using tunenest.Domain.Entities.Artists;

namespace tunenest.Domain.Entities.Users
{
    public class UserFollowArtist
    {
        public UserFollowArtist()
        {
        }

        public Guid user_id { get; set; }
        public long artist_id { get; set; }

        public virtual Artist artist_Artists { get; set; }
        public virtual User user_Users { get; set; }
    }
}
