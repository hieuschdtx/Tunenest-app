using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tunenest.Domain.Entities.Songs;

namespace tunenest.Persistence.Data.EntityConfigurations
{
    public class SongEntityConfiguration
    : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            // table
            builder.ToTable("songs", "public");

            // key
            builder.HasKey(t => t.id);

            // properties
            builder.Property(t => t.id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("character varying(255)")
                .HasMaxLength(255);

            builder.Property(t => t.alias)
                .IsRequired()
                .HasColumnName("alias")
                .HasColumnType("character varying(255)")
                .HasMaxLength(255);

            builder.Property(t => t.avatar_url)
                .IsRequired()
                .HasColumnName("avatar_url")
                .HasColumnType("text");

            builder.Property(t => t.release_date)
                .IsRequired()
                .HasColumnName("release_date")
                .HasColumnType("date");

            builder.Property(t => t.view)
                .IsRequired()
                .HasColumnName("view")
                .HasColumnType("bigint");

            builder.Property(t => t.description)
                .HasColumnName("description")
                .HasColumnType("text")
                .HasDefaultValueSql("'Đang cập nhật'::text");

            builder.Property(t => t.duration)
                .IsRequired()
                .HasColumnName("duration")
                .HasColumnType("integer");

            builder.Property(t => t.lyric)
                .HasColumnName("lyric")
                .HasColumnType("text")
                .HasDefaultValueSql("'Đang cập nhật.'::text");

            builder.Property(t => t.album_id)
                .IsRequired()
                .HasColumnName("album_id")
                .HasColumnType("bigint");

            builder.Property(t => t.language)
                .IsRequired()
                .HasColumnName("language")
                .HasColumnType("character varying(255)")
                .HasMaxLength(255)
                .HasDefaultValueSql("'Đang cập nhật.'::character varying");

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

            // relationships
            builder.HasOne(t => t.album_Albums)
                .WithMany(t => t.album_Songs)
                .HasForeignKey(d => d.album_id)
                .HasConstraintName("fk_songs_album_id");
        }
    }
}
