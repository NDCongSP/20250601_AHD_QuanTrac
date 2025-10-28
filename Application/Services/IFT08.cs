using Application.DTOs.Request;

namespace Application.Services
{
    [BasePath(ApiRoutes.FT08.BasePath)]
    public interface IFT08 : IRepository<Guid, FT08_FilesManagement>
    {
        [Get(ApiRoutes.FT08.GetPdfAsBase64Async)]
        Task<Result<string>> GetPdfAsBase64Async([Query] string pathFile);

        [Post(ApiRoutes.FT08.UploadPdfFileAsync)]
        Task<Result<FT08_FilesManagement>> UploadPdfFileAsync([Body] UploadPdfFileRequest model);

        [Post(ApiRoutes.FT08.CreateFolder)]
        Task<Result<bool>> CreateFolderAsync([Body] CreateFolderRequest model);

        [Post(ApiRoutes.FT08.RenameItem)]
        Task<Result<bool>> RenameItemAsync([Body] RenameItemRequest model);

        [Post(ApiRoutes.FT08.DeleteItem)]
        Task<Result<bool>> DeleteItemAsync([Body] DeleteItemRequest model);

        [Get(ApiRoutes.FT08.GetFolderTree)]
        Task<Result<List<FolderTreeItem>>> GetFolderTreeAsync();
    }
}
