using Application.Services;
using Infrastructure.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FT07Controller : BaseController<Guid, FT07_ChartMucNuoc>, IFT07
    {
        readonly Repository _repository;
        public FT07Controller(Repository repository) : base(repository.SFT07s)
        {
            _repository = repository;
        }
    }
}
