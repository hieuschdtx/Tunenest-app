using tunenest.Domain.Entities.Artists;

namespace tunenest.Domain.Entities.Albums
{
    public class AlbumArtist
    {
        public AlbumArtist()
        {
        }

        public long album_id { get; set; }
        public long artist_id { get; set; }

        public virtual Album album_Albums { get; set; }
        public virtual Artist artist_Artists { get; set; }
    }
}
