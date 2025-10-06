using Application.Services;
using Domain.Models;
using Infrastructure.Repositories;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FT03Controller : BaseController<Guid, FT03>, IFT03
    {
        readonly Repository _repository;
        public FT03Controller(Repository repository) : base(repository.SFT03s)
        {
            _repository = repository;
        }

        [HttpGet(ApiRoutes.FT03.GetByFromDateToDateAsync)]
        public async Task<Result<List<FT03DataPoint>>> GetByFromDateToDateAsync(
            [FromQuery] DateTime? fromDate = null, 
            [FromQuery] DateTime? toDate = null)
        {   
            return await _repository.SFT03s.GetByFromDateToDateAsync(fromDate, toDate);
        }
    }
}
