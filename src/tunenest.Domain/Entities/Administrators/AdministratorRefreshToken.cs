using tunenest.Domain.Commons;

namespace tunenest.Domain.Entities.Administrators
{
    public class AdministratorRefreshToken : Entity<long>
    {
        public AdministratorRefreshToken()
        {
        }

        public AdministratorRefreshToken(string refresh_token, Guid admin_id)
        {
            this.refresh_token = refresh_token;
            this.expires_at = DateTime.Now.AddDays(30);
            this.admin_id = admin_id;
        }

        public string refresh_token { get; private set; }
        public DateTime expires_at { get; private set; }
        public Guid admin_id { get; private set; }

        public virtual Administrator admin_Administrators { get; set; }
    }
}
