using tunenest.Domain.Entities.Albums;

namespace tunenest.Domain.Entities.Users
{
    public class UserFollowAlbum
    {
        public UserFollowAlbum()
        {
        }

        public Guid user_id { get; set; }
        public long album_id { get; set; }

        public virtual Album album_Albums { get; set; }
        public virtual User user_Users { get; set; }
    }
}
