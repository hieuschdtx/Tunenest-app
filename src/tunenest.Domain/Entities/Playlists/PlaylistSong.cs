using tunenest.Domain.Entities.Songs;

namespace tunenest.Domain.Entities.Playlists
{
    public class PlaylistSong
    {
        public PlaylistSong()
        {
        }

        public long playlist_id { get; set; }
        public long song_id { get; set; }

        public virtual Playlist playlist_Playlists { get; set; }
        public virtual Song song_Songs { get; set; }
    }
}
