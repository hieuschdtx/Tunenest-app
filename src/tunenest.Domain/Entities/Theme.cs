using tunenest.Domain.Commons;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Entities.Playlists;

namespace tunenest.Domain.Entities
{
    public class Theme : Entity<long>, IAuditableEntity
    {
        public Theme()
        {
            theme_PlaylistsThemes = new HashSet<PlaylistTheme>();
        }

        public string name { get; set; }
        public string alias { get; set; }
        public bool disable { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? modified_at { get; set; }

        public virtual ICollection<PlaylistTheme> theme_PlaylistsThemes { get; set; }
    }
}
