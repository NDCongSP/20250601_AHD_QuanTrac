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

        public Task<Result<List<FT08_FilesManagement>>> GetAllAsync()
        {
            throw new NotImplementedException();
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
    }
}
