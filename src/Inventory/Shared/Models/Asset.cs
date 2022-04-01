namespace Inventory.Shared
{
    public class Asset : DataModelBase<Asset>
    {
        public Asset() : base() { }

        public Asset(string id) : base(id) { }               

        public string? Name { get; set; }
        public string? Color { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? PurchasePrice { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? InServiceDate { get; set; }
        public DateTime? EndOfServiceDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? ManufactureId { get; set; }
        public Manufacture? Manufacture { get; set; }

        public static implicit operator Asset(AssetViewModel m) => new(m.Id)
        {
            Name = m.Name,
            Color = m.Color,
            PurchaseDate = m.PurchaseDate,
            PurchasePrice = m.PurchasePrice,
            SerialNumber = m.SerialNumber,
            InServiceDate = m.InServiceDate,
            EndOfServiceDate = m.EndOfServiceDate,
            ImageUrl = m.ImageUrl,
            IsDeleted = m.IsDeleted,
            Created = m.Created,
            Modified = m.Modified            
        };
    }
}