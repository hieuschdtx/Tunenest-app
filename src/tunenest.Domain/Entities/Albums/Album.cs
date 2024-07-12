using tunenest.Domain.Commons;
using tunenest.Domain.Entities.Songs;
using tunenest.Domain.Entities.Users;

namespace tunenest.Domain.Entities.Albums
{
    public class Album : Entity<long>
    {
        public Album()
        {
            album_AlbumsArtists = new HashSet<AlbumArtist>();
            album_Songs = new HashSet<Song>();
            album_UsersFollowAlbums = new HashSet<UserFollowAlbum>();
        }

        public string name { get; set; }
        public string alias { get; set; }
        public string avatar_url { get; set; }
        public DateOnly release_date { get; set; }
        public string description { get; set; }
        public string tag { get; set; }
        public string producer { get; set; }
        public bool disable { get; set; }
        public decimal duration { get; set; }

        public virtual ICollection<AlbumArtist> album_AlbumsArtists { get; set; }
        public virtual ICollection<Song> album_Songs { get; set; }
        public virtual ICollection<UserFollowAlbum> album_UsersFollowAlbums { get; set; }
    }
}
