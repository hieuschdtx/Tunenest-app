using tunenest.Domain.Commons;

namespace tunenest.Domain.Entities.Users
{
    public class User : Entity<Guid>
    {
        public User()
        {
            user_UserFollowSongs = new HashSet<UserFollowSong>();
            user_UsersFollowAlbums = new HashSet<UserFollowAlbum>();
            user_UsersFollowArtists = new HashSet<UserFollowArtist>();
            user_UsersFollowPlaylists = new HashSet<UserFollowPlaylist>();
            user_UsersRefreshTokens = new HashSet<UserRefreshToken>();
        }

        public string user_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public DateOnly dob { get; set; }
        public bool gender { get; set; }
        public string avatar_url { get; set; }
        public string password { get; set; }
        public DateTime? lockout_end { get; set; }
        public int? access_failed_count { get; set; }
        public Guid role_id { get; set; }
        public bool lock_acc { get; set; }

        public virtual Role role_Roles { get; set; }
        public virtual ICollection<UserFollowSong> user_UserFollowSongs { get; set; }
        public virtual ICollection<UserFollowAlbum> user_UsersFollowAlbums { get; set; }
        public virtual ICollection<UserFollowArtist> user_UsersFollowArtists { get; set; }
        public virtual ICollection<UserFollowPlaylist> user_UsersFollowPlaylists { get; set; }
        public virtual ICollection<UserRefreshToken> user_UsersRefreshTokens { get; set; }
    }
}
