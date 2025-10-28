using Application.DTOs.Request;
using Application.Services;
using Infrastructure.Repositories;
using Application.Extentions;
using Domain.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FT08Controller : BaseController<Guid, FT08_FilesManagement>, IFT08
    {
        readonly Repository _repository;
        public FT08Controller(Repository repository):base(repository.SFT08s)
        {
            _repository = repository;
        }

        [HttpGet(ApiRoutes.FT08.GetPdfAsBase64Async)]
        public async Task<Result<string>> GetPdfAsBase64Async([Path] string pathFile)
        {
            return await _repository.SFT08s.GetPdfAsBase64Async(pathFile);
        }

        [HttpPost(ApiRoutes.FT08.UploadPdfFileAsync)]
        public async Task<Result<FT08_FilesManagement>> UploadPdfFileAsync([Body] UploadPdfFileRequest model)
        {
            return await _repository.SFT08s.UploadPdfFileAsync(model);
        }
    }
}
