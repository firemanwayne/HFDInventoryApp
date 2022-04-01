using System.ComponentModel.DataAnnotations;

namespace Inventory.Shared
{
    public class EmployeeViewModel : ViewModelBase<EmployeeViewModel>
    {
        public EmployeeViewModel() : base() { }
        public EmployeeViewModel(string Id) : base(Id) { }

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Rank")]
        public string? Rank { get; set; }

        [Required]
        [Display(Name = "Payroll Number")]
        public string? PayrollNumber { get; set; }

        [Required]
        [Display(Name = "Station")]
        public string? StationId { get; set; }

        public string? StationName { get; set; }

        public IEnumerable<StationViewModel>? Stations { get; set; }

        public void LoadLocations(IEnumerable<StationViewModel> m)
        {
            Stations = m.OrderBy(a => a.Name);

            OnPropertyChanged();
        }
    }
}
