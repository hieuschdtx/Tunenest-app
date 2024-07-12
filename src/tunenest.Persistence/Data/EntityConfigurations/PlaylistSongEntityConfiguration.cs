using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Playlists;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class PlaylistSongEntityConfiguration
    : IEntityTypeConfiguration<PlaylistSong>
    {
        public void Configure(EntityTypeBuilder<PlaylistSong> builder)
        {
            // table
            builder.ToTable("playlists_songs", "public");

            // key
            builder.HasKey(t => new { t.playlist_id, t.song_id });

            // properties
            builder.Property(t => t.playlist_id)
                .IsRequired()
                .HasColumnName("playlist_id")
                .HasColumnType("bigint");

            builder.Property(t => t.song_id)
                .IsRequired()
                .HasColumnName("song_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.playlist_Playlists)
                .WithMany(t => t.playlist_PlaylistsSongs)
                .HasForeignKey(d => d.playlist_id)
                .HasConstraintName("fk_playlists_songs_playlist_id");

            builder.HasOne(t => t.song_Songs)
                .WithMany(t => t.song_PlaylistsSongs)
                .HasForeignKey(d => d.song_id)
                .HasConstraintName("fk_playlists_songs_song_id");
        }
    }
}
