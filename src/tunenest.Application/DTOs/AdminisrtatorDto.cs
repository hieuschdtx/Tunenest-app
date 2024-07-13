using tunenest.Application.Commons.Mappings;
using tunenest.Domain.Entities.Administrators;

namespace tunenest.Application.DTOs
{
    public class AdminisrtatorDto : IMapFrom<Administrator>
    {
        public Guid id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string dob { get; set; }
        public bool gender { get; set; }
        public string? avatar_url { get; set; }
        public string password { get; set; }
        public string permission { get; set; }
        public DateTime? lockout_end { get; set; }
        public int access_failed_count { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? modified_at { get; set; }
        public Guid role_id { get; set; }
        public bool lock_acc { get; set; }
    }
}
