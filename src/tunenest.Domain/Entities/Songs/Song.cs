using tunenest.Domain.Commons;
using tunenest.Domain.Entities.Albums;
using tunenest.Domain.Entities.Artists;
using tunenest.Domain.Entities.Playlists;
using tunenest.Domain.Entities.Users;

namespace tunenest.Domain.Entities.Songs
{
    public class Song : Entity<long>
    {
        public Song()
        {
            song_ArtistsSongs = new HashSet<ArtistSong>();
            song_PlaylistsSongs = new HashSet<PlaylistSong>();
            song_SongsGenres = new HashSet<SongGenres>();
            song_UserFollowSongs = new HashSet<UserFollowSong>();
        }

        public string name { get; set; }
        public string alias { get; set; }
        public string avatar_url { get; set; }
        public DateOnly release_date { get; set; }
        public long view { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public string lyric { get; set; }
        public long album_id { get; set; }
        public string language { get; set; }
        public bool disable { get; set; }

        public virtual Album album_Albums { get; set; }
        public virtual ICollection<ArtistSong> song_ArtistsSongs { get; set; }
        public virtual ICollection<PlaylistSong> song_PlaylistsSongs { get; set; }
        public virtual ICollection<SongGenres> song_SongsGenres { get; set; }
        public virtual ICollection<UserFollowSong> song_UserFollowSongs { get; set; }
    }
}
