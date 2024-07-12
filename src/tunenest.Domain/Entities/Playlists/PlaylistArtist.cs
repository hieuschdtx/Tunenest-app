using tunenest.Domain.Entities.Artists;

namespace tunenest.Domain.Entities.Playlists
{
    public class PlaylistArtist
    {
        public PlaylistArtist()
        {
        }

        public long artist_id { get; set; }
        public long playlist_id { get; set; }

        public virtual Artist artist_Artists { get; set; }
        public virtual Playlist playlist_Playlists { get; set; }
    }
}
