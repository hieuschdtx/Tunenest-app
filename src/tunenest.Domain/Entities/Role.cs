using tunenest.Domain.Commons;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Entities.Administrators;
using tunenest.Domain.Entities.Users;

namespace tunenest.Domain.Entities
{
    public class Role : Entity<Guid>, IAuditableEntity
    {
        public Role()
        {
            role_Administrators = new HashSet<Administrator>();
            role_Users = new HashSet<User>();
        }

        public Role(Guid id, string name, bool disable, string description = "Đang cập nhật")
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.disable = disable;
            this.created_at = DateTime.Now;
        }

        public string name { get; private set; }
        public string description { get; private set; }
        public bool disable { get; private set; }
        public DateTime created_at { get; set; }
        public DateTime? modified_at { get; set; }

        public virtual ICollection<Administrator> role_Administrators { get; set; }
        public virtual ICollection<User> role_Users { get; set; }

        public void CreateModifiedAt()
        {
            modified_at = DateTime.Now;
        }
    }
}
