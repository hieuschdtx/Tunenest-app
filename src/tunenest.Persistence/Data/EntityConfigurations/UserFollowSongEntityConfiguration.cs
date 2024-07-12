using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Users;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class UserFollowSongEntityConfiguration
    : IEntityTypeConfiguration<UserFollowSong>
    {
        public void Configure(EntityTypeBuilder<UserFollowSong> builder)
        {
            // table
            builder.ToTable("user_follow_songs", "public");

            // key
            builder.HasKey(t => new { t.user_id, t.song_id });

            // properties
            builder.Property(t => t.user_id)
                .IsRequired()
                .HasColumnName("user_id")
                .HasColumnType("uuid");

            builder.Property(t => t.song_id)
                .IsRequired()
                .HasColumnName("song_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.user_Users)
                .WithMany(t => t.user_UserFollowSongs)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("fk_user_follow_songs_user_id");

            builder.HasOne(t => t.song_Songs)
                .WithMany(t => t.song_UserFollowSongs)
                .HasForeignKey(d => d.song_id)
                .HasConstraintName("fk_user_follow_songs_song_id");
        }
    }
}
