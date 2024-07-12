using tunenest.Domain.Commons;
using tunenest.Domain.Entities.Playlists;

namespace tunenest.Domain.Entities
{
    public class Theme : Entity<long>
    {
        public Theme()
        {
            theme_PlaylistsThemes = new HashSet<PlaylistTheme>();
        }

        public string name { get; set; }
        public string alias { get; set; }
        public bool disable { get; set; }

        public virtual ICollection<PlaylistTheme> theme_PlaylistsThemes { get; set; }
    }
}
