using tunenest.Domain.Commons;

namespace tunenest.Domain.Entities.Administrators
{
    public class AdministratorRefreshToken : Entity<long>
    {
        public AdministratorRefreshToken()
        {
        }

        public string refresh_token { get; set; }
        public DateTime expires_at { get; set; }
        public Guid admin_id { get; set; }

        public virtual Administrator admin_Administrators { get; set; }
    }
}
