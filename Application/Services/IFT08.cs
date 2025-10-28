using Application.DTOs.Request;
using Microsoft.AspNetCore.Components;
using RestEase;

namespace Application.Services
{
    [BasePath(ApiRoutes.FT08.BasePath)]
    public interface IFT08 : IRepository<Guid, FT08_FilesManagement>
    {
        [Get(ApiRoutes.FT08.GetPdfAsBase64Async)]
        Task<Result<string>> GetPdfAsBase64Async([Query] string pathFile);

        [Post(ApiRoutes.FT08.UploadPdfFileAsync)]
        Task<Result<FT08_FilesManagement>> UploadPdfFileAsync([Body] UploadPdfFileRequest model);
    }
}
