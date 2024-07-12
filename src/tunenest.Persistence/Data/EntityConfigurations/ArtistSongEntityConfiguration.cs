using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Artists;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class ArtistSongEntityConfiguration
    : IEntityTypeConfiguration<ArtistSong>
    {
        public void Configure(EntityTypeBuilder<ArtistSong> builder)
        {
            // table
            builder.ToTable("artists_songs", "public");

            // key
            builder.HasKey(t => new { t.artist_id, t.song_id });

            // properties
            builder.Property(t => t.artist_id)
                .IsRequired()
                .HasColumnName("artist_id")
                .HasColumnType("bigint");

            builder.Property(t => t.song_id)
                .IsRequired()
                .HasColumnName("song_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.artist_Artists)
                .WithMany(t => t.artist_ArtistsSongs)
                .HasForeignKey(d => d.artist_id)
                .HasConstraintName("fk_artists_songs_artist_id");

            builder.HasOne(t => t.song_Songs)
                .WithMany(t => t.song_ArtistsSongs)
                .HasForeignKey(d => d.song_id)
                .HasConstraintName("fk_artists_songs_song_id");
        }
    }
}
