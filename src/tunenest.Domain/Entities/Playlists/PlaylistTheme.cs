namespace tunenest.Domain.Entities.Playlists
{
    public class PlaylistTheme
    {
        public PlaylistTheme()
        {
        }

        public long playlist_id { get; set; }
        public long theme_id { get; set; }

        public virtual Playlist playlist_Playlists { get; set; }
        public virtual Theme theme_Themes { get; set; }
    }
}
