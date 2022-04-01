using Inventory.Server.Data;
using Inventory.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        readonly ApplicationDbContext ctx;

        public EmployeesController(ApplicationDbContext ctx) : base()
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public async IAsyncEnumerable<EmployeeViewModel> Get()
        {
            var emps = await ctx
            .Users
            .Include(a => a.Station)
            .ToListAsync();

            foreach(var e in emps)
            {
                yield return new EmployeeViewModel(e.Id)
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Rank = e.Rank,
                    PayrollNumber = e.PayrollNumber,
                    StationId = e.StationId,
                    StationName = e.Station?.Name,
                };
            }
        }
    }
}
