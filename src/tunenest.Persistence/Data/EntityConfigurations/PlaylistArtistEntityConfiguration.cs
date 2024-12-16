using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Playlists;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class PlaylistArtistEntityConfiguration
    : IEntityTypeConfiguration<PlaylistArtist>
    {
        public void Configure(EntityTypeBuilder<PlaylistArtist> builder)
        {
            // table
            builder.ToTable("playlists_artists", "public");

            // key
            builder.HasKey(t => new { t.artist_id, t.playlist_id });

            // properties
            builder.Property(t => t.artist_id)
                .IsRequired()
                .HasColumnName("artist_id")
                .HasColumnType("bigint");

            builder.Property(t => t.playlist_id)
                .IsRequired()
                .HasColumnName("playlist_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.artist_Artists)
                .WithMany(t => t.artist_PlaylistsArtists)
                .HasForeignKey(d => d.artist_id)
                .HasConstraintName("fk_playlists_artists_artist_id");

            builder.HasOne(t => t.playlist_Playlists)
                .WithMany(t => t.playlist_PlaylistsArtists)
                .HasForeignKey(d => d.playlist_id)
                .HasConstraintName("fk_playlists_artists_playlist_id");
        }
    }
}
