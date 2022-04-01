using System.ComponentModel.DataAnnotations;

namespace Inventory.Shared
{
    public class StationViewModel : ViewModelBase<StationViewModel>
    {
        public StationViewModel() : base()
        { }

        public StationViewModel(string? name, int streetNumber, string? streetName, string? state, string? city)
        {
            Name = name;
            StreetNumber = streetNumber;
            StreetName = streetName;
            State = state;
            City = city;
        }

        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Street Number")]
        public int StreetNumber { get; set; }

        [Required]
        [Display(Name = "Street Name")]
        public string? StreetName { get; set; }

        [Required]
        [Display(Name = "State")]
        public string? State { get; set; }

        [Required]
        [Display(Name = "City")]
        public string? City { get; set; }       

        public IList<string> States { get; set; } = new List<string>()
        {
            "Alabama", "Alaska","Arizona","Arkansas","California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho",
            "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi",
            "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio",
            "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia",
            "Washington", "West Virginia", "Wisconsin", "Wyoming"
        };        

        public void View(StationViewModel model)
        {
            if (model != null)
            {
                Id = model.Id;
                Name = model.Name;
                StreetNumber = model.StreetNumber;
                StreetName = model.StreetName;
                State = model.State;
                City = model.City;
                IsDeleted = model.IsDeleted;
                Modified = model.Modified;
                Created = model.Created;
            }
        }               
    }
}