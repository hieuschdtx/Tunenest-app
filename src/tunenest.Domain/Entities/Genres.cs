using tunenest.Domain.Commons;
using tunenest.Domain.Entities.Songs;

namespace tunenest.Domain.Entities
{
    public class Genres : Entity<long>
    {
        public Genres()
        {
            genres_SongsGenres = new HashSet<SongGenres>();
        }

        public string name { get; set; }
        public string alias { get; set; }
        public bool disable { get; set; }

        public virtual ICollection<SongGenres> genres_SongsGenres { get; set; }
    }
}
