using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Users;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class UsersRefreshTokenConfiguration
    : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            // table
            builder.ToTable("users_refresh_token", "public");

            // key
            builder.HasKey(t => t.id);

            // properties
            builder.Property(t => t.id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.refresh_token)
                .IsRequired()
                .HasColumnName("refresh_token")
                .HasColumnType("text");

            builder.Property(t => t.expires_at)
                .IsRequired()
                .HasColumnName("expires_at")
                .HasColumnType("timestamp without time zone");

            builder.Property(t => t.user_id)
                .IsRequired()
                .HasColumnName("user_id")
                .HasColumnType("uuid");

            // relationships
            builder.HasOne(t => t.user_Users)
                .WithMany(t => t.user_UsersRefreshTokens)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("fk_users_id");
        }
    }
}
