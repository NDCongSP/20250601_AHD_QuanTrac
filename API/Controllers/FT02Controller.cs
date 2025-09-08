using Application.Services;
using Infrastructure.Repositories;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FT02Controller : BaseController<Guid, FT02>, IFT02
    {
        readonly Repository _repository;
        public FT02Controller(Repository repository) : base(repository.SFT02s)
        {
            _repository = repository;
        }

        [HttpGet(ApiRoutes.FT02.GetRealtimeDisplay)]

        public async Task<List<RealtimeDisplayModel>> GetFirstOrDefaultRealTimeDisplay()
        {   
            return await _repository.SFT02s.GetFirstOrDefaultRealTimeDisplay();
        }
    }
}
