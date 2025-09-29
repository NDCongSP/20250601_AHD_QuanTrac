using Application.Services;
using Infrastructure.Repositories;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FT05Controller : BaseController<Guid, FT05_ChartHoChua>, IFT05
    {
        readonly Repository _repository;
        public FT05Controller(Repository repository) : base(repository.SFT05s)
        {
            _repository = repository;
        }
    }
}
