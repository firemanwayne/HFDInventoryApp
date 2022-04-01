namespace Inventory.Shared
{
    public class Manufacture : DataModelBase<Manufacture>
    {
        public Manufacture() { }

        public Manufacture(string id) : base(id) { }

        public Manufacture(ManufactureViewModel m) : base(m.Id)
        {
            Name = m.Name;
            Created = m.Created;
            Modified = m.Modified;
            IsDeleted = m.IsDeleted;
        }

        public string? Name { get; set; }

        public ICollection<Asset> Assets { get; set; } = new List<Asset>();

        public static implicit operator Manufacture(ManufactureViewModel m) => new(m.Id)
        {
            Name = m.Name,
            Created = m.Created,
            Modified = m.Modified,
            IsDeleted = m.IsDeleted,
        };
    }
}
