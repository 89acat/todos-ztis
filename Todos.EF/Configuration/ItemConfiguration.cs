using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todos.EF.Entities;
using Todos.EF.Extensions;

namespace Todos.EF.Configuration;

public class ItemConfiguration : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.ToTable(nameof(ItemEntity).TableName());
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Description);
        builder.Property(x => x.IsCompleted).IsRequired();
    }
}