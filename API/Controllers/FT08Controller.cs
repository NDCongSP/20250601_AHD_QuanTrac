using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FT08Controller : BaseController<Guid, FT08_FilesManagement>, IFT08
    {
        readonly Repository _repository;

        public FT08Controller(Repository repository) : base(repository.SFT08s)
        {
            _repository = repository;
        }

        [HttpGet(ApiRoutes.FT08.GetFolderTree)]
        public async Task<Result<List<FolderTreeItem>>> GetFolderTreeAsync()
        {
            return await _repository.SFT08s.GetFolderTreeAsync();
        }

        [HttpGet(ApiRoutes.FT08.GetPdfAsBase64Async)]
        public async Task<Result<string>> GetPdfAsBase64Async([FromQuery] string pathFile)
        {
            return await _repository.SFT08s.GetPdfAsBase64Async(pathFile);
        }

        [HttpPost(ApiRoutes.FT08.UploadPdfFileAsync)]
        public async Task<Result<FT08_FilesManagement>> UploadPdfFileAsync([FromBody] UploadPdfFileRequest model)
        {
            return await _repository.SFT08s.UploadPdfFileAsync(model);
        }

        [HttpPost(ApiRoutes.FT08.CreateFolder)]
        public async Task<Result<bool>> CreateFolderAsync([FromBody] CreateFolderRequest model)
        {
            return await _repository.SFT08s.CreateFolderAsync(model);
        }

        [HttpPost(ApiRoutes.FT08.RenameItem)]
        public async Task<Result<bool>> RenameItemAsync([FromBody] RenameItemRequest model)
        {
            return await _repository.SFT08s.RenameItemAsync(model);
        }

        [HttpPost(ApiRoutes.FT08.DeleteItem)]
        public async Task<Result<bool>> DeleteItemAsync([FromBody] DeleteItemRequest model)
        {
            return await _repository.SFT08s.DeleteItemAsync(model);
        }
    }
}
