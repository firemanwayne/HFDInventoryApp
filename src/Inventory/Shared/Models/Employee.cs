using Microsoft.AspNetCore.Identity;

namespace Inventory.Shared
{
    public class Employee : IdentityUser
    {
        public Employee() : base() { }

        public Employee(string id) : base(id) { }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Rank { get; set; }

        public string? PayrollNumber { get; set; }

        public string? StationId { get; set; }

        public Station? Station { get; set; }

        public static implicit operator EmployeeViewModel(Employee e)
        {
            return new EmployeeViewModel(e.Id)
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