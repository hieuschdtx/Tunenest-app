using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Users;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class UserFollowAlbumEntityConfiguration
    : IEntityTypeConfiguration<UserFollowAlbum>
    {
        public void Configure(EntityTypeBuilder<UserFollowAlbum> builder)
        {
            // table
            builder.ToTable("users_follow_albums", "public");

            // key
            builder.HasKey(t => new { t.user_id, t.album_id });

            // properties
            builder.Property(t => t.user_id)
                .IsRequired()
                .HasColumnName("user_id")
                .HasColumnType("uuid");

            builder.Property(t => t.album_id)
                .IsRequired()
                .HasColumnName("album_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.user_Users)
                .WithMany(t => t.user_UsersFollowAlbums)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("fk_users_follow_albums_user_id");

            builder.HasOne(t => t.album_Albums)
                .WithMany(t => t.album_UsersFollowAlbums)
                .HasForeignKey(d => d.album_id)
                .HasConstraintName("fk_users_follow_albums_album_id");
        }
    }
}
