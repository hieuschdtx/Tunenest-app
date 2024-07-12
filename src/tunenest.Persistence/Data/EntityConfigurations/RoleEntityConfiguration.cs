using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class RoleEntityConfiguration
    : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // table
            builder.ToTable("roles", "public");

            // key
            builder.HasKey(t => t.id);

            // properties
            builder.Property(t => t.id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("uuid");

            builder.Property(t => t.name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("character varying(255)")
                .HasMaxLength(255);

            builder.Property(t => t.description)
                .HasColumnName("description")
                .HasColumnType("character varying(255)")
                .HasMaxLength(255)
                .HasDefaultValueSql("'Đang cập nhật'::character varying");

            builder.Property(t => t.disable)
                .IsRequired()
                .HasColumnName("disable")
                .HasColumnType("boolean");

            builder.Property(t => t.created_at)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(t => t.modified_at)
                .HasColumnName("modified_at")
                .HasColumnType("timestamp without time zone");
        }
    }
}
