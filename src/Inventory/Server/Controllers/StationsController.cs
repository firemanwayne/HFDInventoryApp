using Inventory.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationsController : ControllerBase
    {
        readonly IRepository<Station> _repository;

        public StationsController(IRepository<Station> _repository)
        {
            this._repository = _repository;
        }

        [HttpGet("{id}")]
        public async Task<Station?> Find(string id) 
            => await _repository.FindAsync(id);        

        [HttpGet]
        public async Task<IEnumerable<Station>> Get() 
            => await _repository.GetAllAsync(null);

        [HttpDelete("{id}")]
        public async Task<IEnumerable<Station>> Delete(string id)
        {
            await _repository.Delete(id);

            return await _repository.GetAllAsync(null);
        }

        [HttpPost]
        public async Task<IEnumerable<Station>> Post([FromBody] StationViewModel loc)
        {
            await _repository.Save(loc);

            return await _repository.GetAllAsync(null);
        }
    }
}