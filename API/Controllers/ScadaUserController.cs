using Application.DTOs.Request.Account;
using Application.Services;
using Infrastructure.Repositories;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ScadaUserController : BaseController<Guid, ScadaUser>, IScadaUser
    {
        readonly Repository _repository;
        public ScadaUserController(Repository repository) : base(repository.SScadaUsers)
        {
            _repository = repository;
        }

        [HttpPost(ApiRoutes.ScadaUser.ChangePassword)]
        public async Task<Result<ScadaUser>> ChangePasswordAsync([Body] ChangePassRequestDTO model)
        {
            return await _repository.SScadaUsers.ChangePasswordAsync(model);
        }

    }
}
