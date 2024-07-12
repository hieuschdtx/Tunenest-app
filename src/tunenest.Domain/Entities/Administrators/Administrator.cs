using System.Text.Json;
using tunenest.Domain.Commons;

namespace tunenest.Domain.Entities.Administrators
{
    public class Administrator : Entity<Guid>
    {
        public Administrator()
        {
            admin_AdminsRefreshTokens = new HashSet<AdministratorRefreshToken>();
        }

        //public Administrator(Guid id, string userName, string email, string phoneNumber, string dob,
        //    bool gender, string? avatarUrl, string password, string permission, int accessFailedCount, Guid roleId, bool lockAcc) : this()
        //{
        //    this.id = id;
        //    this.user_name = userName;
        //    this.email = email;
        //    this.phone_number = phoneNumber;
        //    this.dob = SetDOB(dob);
        //    this.gender = gender;
        //    this.avatar_url = avatarUrl;
        //    this.password = SetPassword(password);
        //    this.permission = SetPermission(permission);
        //    this.lockout_end = null;
        //    this.access_failed_count = accessFailedCount;
        //    this.role_id = roleId;
        //    this.lock_acc = lockAcc;
        //    this.created_at = DateTime.Now;
        //}

        public string user_name { get; private set; }
        public string email { get; private set; }
        public string phone_number { get; private set; }
        public DateOnly dob { get; private set; }
        public bool gender { get; private set; }
        public string? avatar_url { get; private set; }
        public string password { get; private set; }
        public JsonDocument permission { get; private set; }
        public DateTime? lockout_end { get; private set; }
        public int access_failed_count { get; private set; }
        public Guid role_id { get; private set; }
        public bool lock_acc { get; private set; }

        public virtual ICollection<AdministratorRefreshToken> admin_AdminsRefreshTokens { get; set; }
        public virtual Role role_Roles { get; set; }
    }
}
