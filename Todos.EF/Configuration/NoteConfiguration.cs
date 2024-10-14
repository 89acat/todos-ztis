using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todos.EF.Entities;
using Todos.EF.Extensions;

namespace Todos.EF.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<NoteEntity>
{
    public void Configure(EntityTypeBuilder<NoteEntity> builder)
    {
        builder.ToTable(nameof(NoteEntity).TableName());
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired(false);
        builder.Property(x => x.CreatedOn).IsRequired().HasDefaultValue(DateTimeOffset.UtcNow);
        builder.Property(x => x.ModifiedOn).IsRequired(false);

        builder.HasOne(e => e.NoteType).WithMany().HasForeignKey(x => x.NoteTypeId).IsRequired();
        builder.HasOne(e => e.NoteStatus).WithMany().HasForeignKey(x => x.NoteStatusId).IsRequired();
        builder.HasMany(e => e.Items).WithOne(e => e.Note).HasForeignKey(x => x.NoteId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
    }
}