using AzureStorage.Services;
using Inventory.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        readonly IRepository<Asset> _repository;
        //readonly ITableStorageService _tableService;
        readonly IBlobStorageServices _blobStorageServices;

        public AssetsController(
            IRepository<Asset> _repository,
            //ITableStorageService _tableService,
            IBlobStorageServices _blobStorageServices)
        {
            this._repository = _repository;
           // this._tableService = _tableService;
            this._blobStorageServices = _blobStorageServices;
        }

        [HttpGet("{id}")]
        public async Task<Asset?> Find(string id)
            => await _repository.FindAsync(id);

        [HttpGet]
        public async Task<IEnumerable<Asset>> Get()
            => await _repository.GetAllAsync(null);

        [HttpDelete("{id}")]
        public async Task<IEnumerable<Asset>> Delete(string id)
        {
            await _repository.Delete(id);

            //_tableService.Remove(nameof(Asset), id);

            return await _repository.GetAllAsync(null);
        }

        [HttpPost]
        public async Task<IEnumerable<Asset>> Post([FromBody] AssetViewModel asset)
        {
            if (asset.UploadRequest != null)
            {
                var result = await _blobStorageServices.Upload(asset.UploadRequest);

                if (result != null)
                    asset.ImageUrl = result.Uri.AbsoluteUri;
            }

            await _repository.Save(asset);

            //_tableService.Save<Asset>(asset);

            return await _repository.GetAllAsync(null);
        }
    }
}
