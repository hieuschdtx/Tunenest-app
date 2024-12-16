using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Playlists;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class PlaylistThemeEntityConfiguration
    : IEntityTypeConfiguration<PlaylistTheme>
    {
        public void Configure(EntityTypeBuilder<PlaylistTheme> builder)
        {
            // table
            builder.ToTable("playlists_themes", "public");

            // key
            builder.HasKey(t => new { t.playlist_id, t.theme_id });

            // properties
            builder.Property(t => t.playlist_id)
                .IsRequired()
                .HasColumnName("playlist_id")
                .HasColumnType("bigint");

            builder.Property(t => t.theme_id)
                .IsRequired()
                .HasColumnName("theme_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.playlist_Playlists)
                .WithMany(t => t.playlist_PlaylistsThemes)
                .HasForeignKey(d => d.playlist_id)
                .HasConstraintName("fk_playlists_themes_playlist_id");

            builder.HasOne(t => t.theme_Themes)
                .WithMany(t => t.theme_PlaylistsThemes)
                .HasForeignKey(d => d.theme_id)
                .HasConstraintName("fk_playlists_themes_theme_id");
        }
    }
}
