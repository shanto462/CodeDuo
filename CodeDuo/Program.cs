using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CodeDuo.Data;
using CodeDuo.Areas.Identity.Data;
using CodeDuo.Hubs;
using Microsoft.AspNet.SignalR;
using CodeDuo.DI;
using CodeDuo.DI.Memory;
using CodeDuo.DI.Sql;
using CodeDuo.DI.Access;
using CodeDuo.DI.Providers;
using CodeDuo.DI.Memory.ConnectionCache;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CodeDuoDbContextConnection");

builder.Services.AddDbContext<CodeDuoDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<CodeDuoUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<CodeDuoDbContext>();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Playground");
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<IConnectionCache, ConnectionCache>();
builder.Services.AddSingleton<ICodeIdProvider, CodeIdProvider>();
builder.Services.AddSingleton<IMemoryDB, MemoryDB>();
builder.Services.AddSingleton<ISqlDb, SqlDb>();
builder.Services.AddSingleton<IAccessDB, AccessDB>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<PlaygroundHub>("/playgroundHub");

GlobalHost.DependencyResolver = new SignalRDependencyResolver(app.Services);

app.Run();
