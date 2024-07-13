using Microsoft.AspNetCore.Http;
using tunenest.Domain.Commons.Interfaces.Commands;
using tunenest.Domain.Helpers;

namespace tunenest.Application.Features.Administrators.Commands.Create
{
    public class CreateAdminCommand : CommandBase<BaseResult, Guid>
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone_number { get; set; }
        public string dob { get; set; }
        public bool gender { get; set; } = false;
        public Permission permission { get; set; }
        public int access_failed_count { get; set; } = 0;
        public IFormFile? avatar_file { get; set; } = null;
        public string role_id { get; set; }
        public bool lock_acc { get; set; } = false;
    }

    public sealed record Permission
    {
        public bool create { get; set; }
        public bool update { get; set; }
        public bool delete { get; set; }
    }
}
