using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Users;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class UserFollowPlaylistEntityConfiguration
    : IEntityTypeConfiguration<UserFollowPlaylist>
    {
        public void Configure(EntityTypeBuilder<UserFollowPlaylist> builder)
        {
            // table
            builder.ToTable("users_follow_playlists", "public");

            // key
            builder.HasKey(t => new { t.user_id, t.playlist_id });

            // properties
            builder.Property(t => t.user_id)
                .IsRequired()
                .HasColumnName("user_id")
                .HasColumnType("uuid");

            builder.Property(t => t.playlist_id)
                .IsRequired()
                .HasColumnName("playlist_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.user_Users)
                .WithMany(t => t.user_UsersFollowPlaylists)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("fk_users_follow_playlists_user_id");

            builder.HasOne(t => t.playlist_Playlists)
                .WithMany(t => t.playlist_UsersFollowPlaylists)
                .HasForeignKey(d => d.playlist_id)
                .HasConstraintName("fk_users_follow_playlists_playlist_id");
        }
    }
}
