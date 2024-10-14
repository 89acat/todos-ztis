using Microsoft.EntityFrameworkCore;
using Todos.Common.Enums;
using Todos.Common.Extensions;
using Todos.EF.Entities;

namespace Todos.EF.DataSeed;

public static class NoteTypeSeeder
{
    public static ModelBuilder SeedNoteTypes(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<NoteTypeEntity>();

        foreach (var type in EnumExtensions.GetValues<NoteType>())
        {
            var data = new NoteTypeEntity
            {
                Id = type,
                Name = type.ToString()
            };

            entity.HasData(data);
        }

        return modelBuilder;
    }
}