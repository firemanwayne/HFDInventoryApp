using System.ComponentModel.DataAnnotations;

namespace Inventory.Shared
{
    public abstract class DataModelBase<T> where T : class
    {
        public DataModelBase()
        {
            Id = Guid
                .NewGuid()
                .ToString();

            IsDeleted = false;           
        }

        public DataModelBase(string id)
        {
            Id = id;
        }

        public DataModelBase(ViewModelBase<T> m)
        {
            Id = m.Id;
            IsDeleted = m.IsDeleted;
            Created = m.Created;
            Modified = m.Modified;
        }

        [Key]
        public string Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public void OnCreated()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
        public void OnUpdated()
        {
            Modified = DateTime.Now;
        }
        public void OnDelete()
        {
            IsDeleted = true;
            Modified = DateTime.Now;
        }
    }
}
