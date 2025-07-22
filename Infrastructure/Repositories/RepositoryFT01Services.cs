using Application.DTOs.Response;
using Application.Extentions;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositoryFT01Services(ApplicationDbContext dbContext,IHttpContextAccessor contextAccessor) : IFT01
    {
        public Task<Result<List<FT01>>> AddRangeAsync([Body] List<FT01> model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT01>> DeleteAsync([Body] FT01 model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT01>> DeleteRangeAsync([Body] List<FT01> model)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<FT01>>> GetAllAsync()
        {
            try
            {
                return await Result<List<FT01>>.SuccessAsync(await dbContext.FT01s.ToListAsync());
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<List<FT01>>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<FT01>> GetByIdAsync([Path] Guid id)
        {
            try
            {
                return await Result<FT01>.SuccessAsync(await dbContext.FT01s.FindAsync(id));
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT01>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<FT01>> InsertAsync([Body] FT01 model)
        {
            try
            {
                //lay thong tin user
                var userInfo = contextAccessor.HttpContext.User.FindFirst("UserId");

                model.CreateAt = DateTime.Now;
                model.CreateOperatorId = userInfo.ToString();
                model.IsDeleted = false;

                await dbContext.FT01s.AddAsync(model);
                await dbContext.SaveChangesAsync();
                return await Result<FT01>.SuccessAsync(model);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT01>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<FT01>> UpdateAsync([Body] FT01 model)
        {
            try
            {
                //lay thong tin user
                var userInfo = contextAccessor.HttpContext.User.FindFirst("UserId");

                model.UpdateAt = DateTime.Now;
                model.UpdateOperatorId = userInfo.ToString();

                dbContext.FT01s.Update(model);
                await dbContext.SaveChangesAsync();
                return await Result<FT01>.SuccessAsync(model);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT01>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }
    }
}
