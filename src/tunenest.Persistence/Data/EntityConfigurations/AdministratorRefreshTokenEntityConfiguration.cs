using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Administrators;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class AdministratorRefreshTokenEntityConfiguration
    : IEntityTypeConfiguration<AdministratorRefreshToken>
    {
        public void Configure(EntityTypeBuilder<AdministratorRefreshToken> builder)
        {
            // table
            builder.ToTable("admins_refresh_token", "public");

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

            builder.Property(t => t.admin_id)
                .IsRequired()
                .HasColumnName("admin_id")
                .HasColumnType("uuid");

            // relationships
            builder.HasOne(t => t.admin_Administrators)
                .WithMany(t => t.admin_AdminsRefreshTokens)
                .HasForeignKey(d => d.admin_id)
                .HasConstraintName("fk_administrators_id");
        }
    }
}
