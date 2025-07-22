using Application.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
