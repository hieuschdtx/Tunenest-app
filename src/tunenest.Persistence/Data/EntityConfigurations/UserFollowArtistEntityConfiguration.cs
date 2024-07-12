using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Users;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class UserFollowArtistEntityConfiguration
    : IEntityTypeConfiguration<UserFollowArtist>
    {
        public void Configure(EntityTypeBuilder<UserFollowArtist> builder)
        {
            // table
            builder.ToTable("users_follow_artists", "public");

            // key
            builder.HasKey(t => new { t.user_id, t.artist_id });

            // properties
            builder.Property(t => t.user_id)
                .IsRequired()
                .HasColumnName("user_id")
                .HasColumnType("uuid");

            builder.Property(t => t.artist_id)
                .IsRequired()
                .HasColumnName("artist_id")
                .HasColumnType("bigint");

            // relationships
            builder.HasOne(t => t.user_Users)
                .WithMany(t => t.user_UsersFollowArtists)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("fk_users_follow_artists_user_id");

            builder.HasOne(t => t.artist_Artists)
                .WithMany(t => t.artist_UsersFollowArtists)
                .HasForeignKey(d => d.artist_id)
                .HasConstraintName("fk_users_follow_artists_artist_id");
        }
    }
}
