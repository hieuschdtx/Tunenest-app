namespace tunenest.Domain.Entities.Songs
{
    public class SongGenres
    {
        public SongGenres()
        {
        }

        public long song_id { get; set; }
        public long genres_id { get; set; }

        public virtual Genres genres_Genres { get; set; }
        public virtual Song song_Songs { get; set; }
    }
}
