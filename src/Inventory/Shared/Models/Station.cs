namespace Inventory.Shared
{
    public class Station : DataModelBase<Station>
    {
        public Station() : base() { }
        public Station(string Id) : base(Id) { }

        public Station(string name, int streetNumber, string streetName, string state, string city) : base()
        {
            Name = name;
            StreetNumber = streetNumber;
            StreetName = streetName;
            State = state;
            City = city;
        }

        public string? Name { get; set; }

        public int StreetNumber { get; set; }

        public string? StreetName { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public static implicit operator Station(StationViewModel s) => new(s.Id)
        {
            Name = s.Name,
            StreetNumber = s.StreetNumber,
            StreetName = s.StreetName,
            State = s.State,
            City = s.City,
            Created = s.Created,
            IsDeleted = s.IsDeleted,
            Modified = s.Modified,
        };
    }
}
