using AzureStorage.Extensions;
using Inventory.Server.Data;
using Inventory.Server.Data.Seed;
using Inventory.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-Inventory.Server-566275C2-02FF-4F8A-BE1A-1C9BD0EF24F4;Trusted_Connection=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<ApplicationDbContext>(o => 
{
    o.UseSqlServer(connectionString, opts => 
    {
        opts.EnableRetryOnFailure();
    })
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging();
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Employee>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddUserManager<UserManager<Employee>>()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddSignInManager<SignInManager<Employee>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<Employee, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));

builder.Services.AddAzureBlobStorage(o =>
{
    o.IsDevelopment = true;
})
  .AddAzureTableStorage(o =>
  {
      o.TableName = "InventoryTable";
      o.IsDevelopment = true;
  });

var app = builder.Build();

await app.CleanUpSeedData();
await app.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();