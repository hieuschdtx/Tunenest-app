using tunenest.Domain.Commons;
using tunenest.Domain.Entities.Songs;

namespace tunenest.Domain.Entities.Artists
{
    public class ArtistSong : Entity<long>
    {
        public ArtistSong()
        {
        }

        public long artist_id { get; set; }
        public long song_id { get; set; }

        public virtual Artist artist_Artists { get; set; }
        public virtual Song song_Songs { get; set; }
    }
}
