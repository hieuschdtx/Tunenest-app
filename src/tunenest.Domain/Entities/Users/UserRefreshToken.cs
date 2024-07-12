using tunenest.Domain.Commons;

namespace tunenest.Domain.Entities.Users
{
    public class UserRefreshToken : Entity<long>
    {
        public UserRefreshToken()
        {
        }

        public string refresh_token { get; set; }
        public DateTime expires_at { get; set; }
        public Guid user_id { get; set; }

        public virtual User user_Users { get; set; }
    }
}
