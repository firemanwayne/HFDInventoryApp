using System.ComponentModel.DataAnnotations;

namespace Inventory.Shared
{
    public class ManufactureViewModel : ViewModelBase<ManufactureViewModel>
    {
        public ManufactureViewModel() : base() { }

        public ManufactureViewModel(string Id) : base(Id) { }

        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }
    }
}
