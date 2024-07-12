using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Songs;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class SongGenresEntityConfiguration
    : IEntityTypeConfiguration<SongGenres>
    {
        public void Configure(EntityTypeBuilder<SongGenres> builder)
        {
            // table
            builder.ToTable("songs_genres", "public");

            // key
            builder.HasKey(t => new { t.song_id, t.genres_id });

            // properties
            builder.Property(t => t.song_id)
                .IsRequired()
                .HasColumnName("song_id")
                .HasColumnType("bigint");

            builder.Property(t => t.genres_id)
                .IsRequired()
                .HasColumnName("genres_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.song_Songs)
                .WithMany(t => t.song_SongsGenres)
                .HasForeignKey(d => d.song_id)
                .HasConstraintName("fk_songs_genres_song_id");

            builder.HasOne(t => t.genres_Genres)
                .WithMany(t => t.genres_SongsGenres)
                .HasForeignKey(d => d.genres_id)
                .HasConstraintName("fk_songs_genres_genre_id");
        }
    }
}
