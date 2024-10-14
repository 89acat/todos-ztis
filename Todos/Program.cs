using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todos.EF.Context;
using Todos.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TodosContextConnection") ??
                       throw new InvalidOperationException("Connection string 'TodosContextConnection' not found.");

builder.Services
    .AddDbContext<TodosDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<TodosDbContext>();

// Add services to the container.
// builder.Services.AddEfServices(@"Server=.\SQLEXPRESS;Database=Todos;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true");
// builder.Services.AddEfServices(@"Server=(localdb)\\Todos;Database=Todos;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true");
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
;
builder.Services.AddScoped<ITodosRepository, TodosRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();