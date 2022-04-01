using AzureStorage;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Shared
{
    public class AssetViewModel : ViewModelBase<AssetViewModel>
    {
        public AssetViewModel() : base() { }

        public AssetViewModel(string Id) : base(Id) { }

        [Required]
        public string? Name { get; set; }
        public string? Color { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        public string? PurchasePrice { get; set; }

        [Required]
        public string? SerialNumber { get; set; }

        [Required]
        public DateTime? InServiceDate { get; set; }
        public DateTime? EndOfServiceDate { get; set; }
        public string? ImageUrl { get; set; }     
        public UploadRequest? UploadRequest { get; set; }

        public void View(AssetViewModel m)
        {
            if (m != null)
            {
                Id = m.Id;
                Name = m.Name;  
                Color = m.Color;
                PurchaseDate = m.PurchaseDate;
                PurchasePrice = m.PurchasePrice;
                SerialNumber = m.SerialNumber;
                InServiceDate = m.InServiceDate;
                EndOfServiceDate = m.EndOfServiceDate;
                ImageUrl = m.ImageUrl;  
                IsDeleted = m.IsDeleted;
                Created = m.Created;
                Modified = m.Modified;                
            }
        }        

        public async Task Upload(IBrowserFile file)
        {
            var buf = new byte[file.Size]; // allocate a buffer to fill with the file's data
            using (var stream = file.OpenReadStream())
            {
                await stream.ReadAsync(buf); // copy the stream to the buffer
            }

            UploadRequest = new() 
            {
                ContainerName = "assetimages",        
                FileName = file.Name,
                Size = file.Size,
                ContentType = file.ContentType,
                Base64data = Convert.ToBase64String(buf)
            };
        }
    }
}
