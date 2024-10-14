using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todos.EF.Entities;
using Todos.EF.Extensions;

namespace Todos.EF.Configuration;

public class NoteStatusConfiguration : IEntityTypeConfiguration<NoteStatusEntity>
{
    public void Configure(EntityTypeBuilder<NoteStatusEntity> builder)
    {
        builder.ToTable(nameof(NoteStatusEntity).TableName());
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
    }
}