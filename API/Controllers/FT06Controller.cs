using Application.Services;
using Infrastructure.Repositories;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FT06Controller : BaseController<Guid, FT06_InterpolationTable>, IFT06
    {
        readonly Repository _repository;
        public FT06Controller(Repository repository) : base(repository.SFT06s)
        {
            _repository = repository;
        }
    }
}
