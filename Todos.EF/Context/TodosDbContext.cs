using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todos.EF.DataSeed;
using Todos.EF.Entities;

namespace Todos.EF.Context;

public class TodosDbContext : IdentityDbContext<IdentityUser>
{     
    public TodosDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodosDbContext).Assembly)
            .SeedNoteStatuses()
            .SeedNoteTypes();
    }
}