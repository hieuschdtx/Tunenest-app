namespace tunenest.Infrastructure.Configurations
{
    public class ClaimInformation
    {
        public ClaimInformation()
        { }

        public ClaimInformation(string id, string userName, string email, string phoneNumber,
            string profilePicture, string permissions, string role)
        {
            this.id = id;
            this.user_name = userName;
            this.email = email;
            this.phone_number = phoneNumber;
            this.profile_picture = profilePicture;
            this.permissions = permissions;
            this.role = role;
        }

        public string id { get; private set; }
        public string user_name { get; private set; }
        public string email { get; private set; }
        public string phone_number { get; private set; }
        public string? profile_picture { get; private set; } = null;
        public string? permissions { get; private set; } = null;
        public string role { get; private set; }
    }
}
