using tunenest.Domain.Commons;
using tunenest.Domain.Entities.Albums;
using tunenest.Domain.Entities.Playlists;
using tunenest.Domain.Entities.Users;

namespace tunenest.Domain.Entities.Artists
{
    public class Artist : Entity<long>
    {
        public Artist()
        {
            artist_AlbumsArtists = new HashSet<AlbumArtist>();
            artist_ArtistsSongs = new HashSet<ArtistSong>();
            artist_PlaylistsArtists = new HashSet<PlaylistArtist>();
            artist_UsersFollowArtists = new HashSet<UserFollowArtist>();
        }

        public string name { get; set; }
        public string alias { get; set; }
        public string avatar_url { get; set; }
        public string thumbnail_url { get; set; }
        public string description { get; set; }
        public int? reward { get; set; }
        public DateOnly? dob { get; set; }
        public string country { get; set; }
        public bool disable { get; set; }
        public virtual ICollection<AlbumArtist> artist_AlbumsArtists { get; set; }
        public virtual ICollection<ArtistSong> artist_ArtistsSongs { get; set; }
        public virtual ICollection<PlaylistArtist> artist_PlaylistsArtists { get; set; }
        public virtual ICollection<UserFollowArtist> artist_UsersFollowArtists { get; set; }
    }
}
