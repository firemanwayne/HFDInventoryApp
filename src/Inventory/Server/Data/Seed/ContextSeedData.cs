using Inventory.Shared;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Inventory.Server.Data.Seed
{
    public static class ContextSeedData
    {
        readonly static Claim DefaultUserClaim = new(issuer: "System", type: "Role", value: "default role", valueType: typeof(string).Name);

        public static async Task SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userStore = scope.ServiceProvider.GetRequiredService<IUserStore<Employee>>();
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
            var _emailStore = GetEmailStore();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
            var _signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<Employee>>();

            if (ctx != null)
                ctx.Database.EnsureCreated();

            await _roleManager.CreateAsync(new IdentityRole("default role"));
            await _roleManager.CreateAsync(new IdentityRole("Chief Officer"));
            await _roleManager.CreateAsync(new IdentityRole("Station Officer"));
            await _roleManager.CreateAsync(new IdentityRole("Engineer/Operator"));
            await _roleManager.CreateAsync(new IdentityRole("Firefighter"));

            await _roleManager.CreateAsync(new IdentityRole("Rescue Division"));
            await _roleManager.CreateAsync(new IdentityRole("Suppression"));

            await AddDefaultUser();
            await AddStations();

            Console.BackgroundColor = ConsoleColor.Black;


            async Task AddDefaultUser()
            {
                try
                {
                    var user = Activator.CreateInstance<Employee>();
                    user.FirstName = "default";
                    user.LastName = "user";
                    user.Rank = "N/A";
                    user.PayrollNumber = "999999";
                    user.EmailConfirmed = true;
                    user.PhoneNumber = "999-999-9999";
                    user.PhoneNumberConfirmed = true;
                    user.LockoutEnabled = false;

                    await _userStore.SetUserNameAsync(user, "defaultUser@houstontx.gov", CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, "defaultUser@houstontx.gov", CancellationToken.None);

                    var result = await _userManager.CreateAsync(user, "HFDrescue123!");

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;

                    _logger.LogInformation("Default User Successfully Created");

                    await _userManager.AddToRoleAsync(user, "default role");
                    await _userManager.AddClaimAsync(user, DefaultUserClaim);

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;

                    _logger.LogInformation("Default User Successfully assigned default role");

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;

                    _logger.LogInformation("Default User Successfully Signed-Out");
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;

                    _logger.LogError($"Default User Failed to be created: {ex.Message}");

                    throw new InvalidOperationException($"Can't create an instance of '{nameof(Employee)}'. " +
                        $"Ensure that '{nameof(Employee)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                        $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
                }
            }

            async Task AddStations()
            {
                if (ctx != null)
                {
                    var dbset = ctx.Set<Station>();

                    var stations = new List<Station>()
                    {
                        new Station("HFD #10", 6600, "Corporate Drive", "Texas", "Houston"),
                        new Station("HFD #11", 460, "T.C. Jester Drive", "Texas", "Houston"),
                        new Station("HFD #42", 1100, "Clinton Drive", "Texas", "Houston")
                    };

                    dbset.AddRange(stations);
                    var result = await ctx.SaveChangesAsync();

                    _logger.LogInformation($"{result} entries successfully saved");
                }
            }

            IUserEmailStore<Employee> GetEmailStore()
            {
                if (!_userManager.SupportsUserEmail)
                    throw new NotSupportedException("The default UI requires a user store with email support.");

                return (IUserEmailStore<Employee>)_userStore;
            }
        }

        public static async Task CleanUpSeedData(this WebApplication app)
        {
            var _ctx = app
                .Services
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            if (_ctx != null)
               await _ctx.Database.EnsureDeletedAsync();
        }
    }
}
