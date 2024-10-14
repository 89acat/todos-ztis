using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Todos.EF.Context;

public class TodosDbContextFactory : IDesignTimeDbContextFactory<TodosDbContext>
{
    public TodosDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TodosDbContext>();
        // optionsBuilder.UseSqlServer(
            // @"Server=.\SQLEXPRESS;Database=Todos;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true");
        optionsBuilder.UseSqlServer(@"Server=tcp:tj-sql-server.database.windows.net,1433;Initial Catalog=Todos;Persist Security Info=False;User ID=tamarajevtic;Password=Test1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        return new TodosDbContext(optionsBuilder.Options);
    }
}