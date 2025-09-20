using Application.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FT01Controller : BaseController<Guid, FT01>, IFT01
    {
        readonly Repository _repository;

        public FT01Controller(Repository repository = null) : base(repository.SFT01s)
        {
            _repository = repository;
        }
        
        [HttpGet(ApiRoutes.FT01.GetConfig)]
        public async Task<Result<ConfigModel>> GetConfigAsync()
        {
            return await _repository.SFT01s.GetConfigAsync();
        }
        
        [HttpPost(ApiRoutes.FT01.UpdateConfig)]
        public async Task<Result<ConfigModel>> UpdateConfigAsync([FromBody] ConfigModel config)
        {
            return await _repository.SFT01s.UpdateConfigAsync(config);
        }
        
        [HttpPost(ApiRoutes.FT01.DeleteLocation)]
        public async Task<Result<bool>> DeleteLocationAsync([Path] int? locationId)
        {
            return await _repository.SFT01s.DeleteLocationAsync(locationId);
        }
        
        [HttpPost(ApiRoutes.FT01.AddOrUpdateLocation)]
        public async Task<Result<LocationInfoModel>> AddOrUpdateLocationAsync([Body] LocationInfoModel location)
        {
            return await _repository.SFT01s.AddOrUpdateLocationAsync(location);
        }
    }
}
