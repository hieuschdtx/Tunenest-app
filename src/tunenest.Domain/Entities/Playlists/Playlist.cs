using tunenest.Domain.Commons;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Entities.Users;

namespace tunenest.Domain.Entities.Playlists
{
    public class Playlist : Entity<long>, IAuditableEntity
    {
        public Playlist()
        {
            playlist_PlaylistsArtists = new HashSet<PlaylistArtist>();
            playlist_PlaylistsSongs = new HashSet<PlaylistSong>();
            playlist_PlaylistsThemes = new HashSet<PlaylistTheme>();
            playlist_UsersFollowPlaylists = new HashSet<UserFollowPlaylist>();
        }

        public string name { get; set; }
        public string alias { get; set; }
        public string avatar_url { get; set; }
        public DateOnly release_date { get; set; }
        public string description { get; set; }
        public bool disable { get; set; }
        public string tag { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? modified_at { get; set; }

        public virtual ICollection<PlaylistArtist> playlist_PlaylistsArtists { get; set; }
        public virtual ICollection<PlaylistSong> playlist_PlaylistsSongs { get; set; }
        public virtual ICollection<PlaylistTheme> playlist_PlaylistsThemes { get; set; }
        public virtual ICollection<UserFollowPlaylist> playlist_UsersFollowPlaylists { get; set; }
    }
}
