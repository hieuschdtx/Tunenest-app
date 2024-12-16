using tunenest.Domain.Entities.Playlists;

namespace tunenest.Domain.Entities.Users
{
    public class UserFollowPlaylist
    {
        public UserFollowPlaylist()
        {
        }

        public Guid user_id { get; set; }
        public long playlist_id { get; set; }

        public virtual Playlist playlist_Playlists { get; set; }
        public virtual User user_Users { get; set; }
    }
}
