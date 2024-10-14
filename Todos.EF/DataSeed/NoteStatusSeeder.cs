using Microsoft.EntityFrameworkCore;
using Todos.Common.Enums;
using Todos.Common.Extensions;
using Todos.EF.Entities;

namespace Todos.EF.DataSeed;

public static class NoteStatusSeeder
{
    public static ModelBuilder SeedNoteStatuses(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<NoteStatusEntity>();

        foreach (var status in EnumExtensions.GetValues<NoteStatus>())
        {
            var data = new NoteStatusEntity
            {
                Id = status,
                Name = status.ToString()
            };

            entity.HasData(data);
        }

        return modelBuilder;
    }
}