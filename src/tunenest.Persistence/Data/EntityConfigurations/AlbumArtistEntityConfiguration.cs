using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Albums;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class AlbumArtistEntityConfiguration
    : IEntityTypeConfiguration<AlbumArtist>
    {
        public void Configure(EntityTypeBuilder<AlbumArtist> builder)
        {
            // table
            builder.ToTable("albums_artists", "public");

            // key
            builder.HasKey(t => new { t.album_id, t.artist_id });

            // properties
            builder.Property(t => t.album_id)
                .IsRequired()
                .HasColumnName("album_id")
                .HasColumnType("bigint");

            builder.Property(t => t.artist_id)
                .IsRequired()
                .HasColumnName("artist_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.album_Albums)
                .WithMany(t => t.album_AlbumsArtists)
                .HasForeignKey(d => d.album_id)
                .HasConstraintName("fk_albums_artists_album_id");

            builder.HasOne(t => t.artist_Artists)
                .WithMany(t => t.artist_AlbumsArtists)
                .HasForeignKey(d => d.artist_id)
                .HasConstraintName("fk_albums_artists_artist_id");
        }
    }
}
