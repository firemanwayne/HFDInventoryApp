using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Inventory.Shared
{
    public abstract class ViewModelBase<T> : INotifyPropertyChanged where T : class
    {
        bool isLoading;

        public ViewModelBase() : base()
        {
            Id = Guid
                .NewGuid()
                .ToString();

            IsDeleted = false;
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        public ViewModelBase(string Id)
        {
            this.Id = Id;
        }        

        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public IEnumerable<T> Models { get; set; } = Enumerable.Empty<T>();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Loading() => IsLoading = true;
        public void NotLoading() => IsLoading = false;

        public bool IsLoading
        {
            get { return isLoading; }
            private set
            {
                isLoading = value;

                OnPropertyChanged();
            }
        }       

        public virtual void ClearModels()
        {
            Models = Enumerable.Empty<T>();

            OnPropertyChanged();
        }
        public virtual void SetModels(IEnumerable<T> models)
        {
            Models = models;

            OnPropertyChanged();
        }
    }
}
