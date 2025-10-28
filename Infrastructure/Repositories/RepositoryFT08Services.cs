using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Extentions;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositoryFT08Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IFT08
    {
        public Task<Result<List<FT08_FilesManagement>>> AddRangeAsync([Body] List<FT08_FilesManagement> model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT08_FilesManagement>> DeleteAsync([Body] FT08_FilesManagement model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT08_FilesManagement>> DeleteRangeAsync([Body] List<FT08_FilesManagement> model)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<FT08_FilesManagement>>> GetAllAsync()
        {
            try
            {
                var data = await Task.FromResult(
                    dbContext.FT08_FilesManagements
                        .OrderBy(x => x.PathFile)
                        .ThenBy(x => x.FileName)
                        .ToList());
                return await Result<List<FT08_FilesManagement>>.SuccessAsync(data, "Successful");
            }
            catch (Exception ex)
            {
                return await Result<List<FT08_FilesManagement>>.FailAsync(new List<string> { ex.Message });
            }
        }

        public Task<Result<FT08_FilesManagement>> GetByIdAsync([Path] Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetPdfAsBase64Async([Path] string pathFile)
        {
            try
            {
                if (string.IsNullOrEmpty(pathFile) || !System.IO.File.Exists(pathFile))
                {
                    var err = new ErrorResponse();
                    err.Errors.Add("Warning", "File not found or path is invalid.");
                    return await Result<string>.FailAsync(JsonConvert.SerializeObject(err));
                }

                // Đọc file PDF
                byte[] pdfBytes = System.IO.File.ReadAllBytes(pathFile);

                // Chuyển thành Base64
                string base64String = Convert.ToBase64String(pdfBytes);

                // Trả về kết quả
                return await Result<string>.SuccessAsync(base64String, "Successful");
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<string>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public Task<Result<FT08_FilesManagement>> InsertAsync([Body] FT08_FilesManagement model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT08_FilesManagement>> UpdateAsync([Body] FT08_FilesManagement model)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<FT08_FilesManagement>> UploadPdfFileAsync([Body] UploadPdfFileRequest model)
        {
            try
            {
                if (model == null)
                {
                    return await Result<FT08_FilesManagement>.FailAsync("Request is null");
                }
                if (string.IsNullOrWhiteSpace(model.PathFile))
                {
                    return await Result<FT08_FilesManagement>.FailAsync("PathFile is required");
                }
                if (string.IsNullOrWhiteSpace(model.FileName))
                {
                    return await Result<FT08_FilesManagement>.FailAsync("FileName is required");
                }
                if (string.IsNullOrWhiteSpace(model.Base64))
                {
                    return await Result<FT08_FilesManagement>.FailAsync("Base64 content is required");
                }

                // Ensure directory exists
                if (!System.IO.Directory.Exists(model.PathFile))
                {
                    System.IO.Directory.CreateDirectory(model.PathFile);
                }

                var fullPath = System.IO.Path.Combine(model.PathFile, model.FileName);

                // Handle possible data URL prefix
                var base64 = model.Base64;
                var commaIdx = base64.IndexOf(",");
                if (commaIdx > -1)
                {
                    base64 = base64[(commaIdx + 1)..];
                }

                byte[] bytes = Convert.FromBase64String(base64);
                await System.IO.File.WriteAllBytesAsync(fullPath, bytes);

                var entity = new FT08_FilesManagement
                {
                    Id = Guid.NewGuid(),
                    PathFile = model.PathFile,
                    FileName = model.FileName,
                    CreateAt = DateTime.Now,
                    IsDeleted = false
                };

                dbContext.FT08_FilesManagements.Add(entity);
                await dbContext.SaveChangesAsync();

                return await Result<FT08_FilesManagement>.SuccessAsync(entity, "Uploaded successfully");
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT08_FilesManagement>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }
    }
}
