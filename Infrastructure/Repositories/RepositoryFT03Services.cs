using Application.DTOs.Response;
using Application.Extentions;
using Application.Services;
using Domain;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestEase;
using System.Globalization;

namespace Infrastructure.Repositories
{
    public class RepositoryFT03Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IFT03
    {
        public Task<Result<List<FT03>>> AddRangeAsync([Body] List<FT03> model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT03>> DeleteAsync([Body] FT03 model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT03>> DeleteRangeAsync([Body] List<FT03> model)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<FT03>>> GetAllAsync()
        {
            try
            {
                return await Result<List<FT03>>.SuccessAsync(await dbContext.FT03s.ToListAsync());
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<List<FT03>>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }
        public async Task<Result<List<FT03DataPoint>>> GetByFromDateToDateAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                // Nếu không chỉ định ngày bắt đầu/kết thúc, sử dụng năm tài chính hiện tại
                if (!fromDate.HasValue || !toDate.HasValue)
                {
                    var now = DateTime.Now;
                    fromDate = now < new DateTime(now.Year, 7, 31) ? 
                        new DateTime(now.Year - 1, 7, 1) : 
                        new DateTime(now.Year, 7, 1);
                    
                    toDate = now < new DateTime(now.Year, 7, 31) ? 
                        new DateTime(now.Year, 6, 30) : 
                        new DateTime(now.Year + 1, 6, 30);
                }

                var data = await dbContext.FT03s
                    .Where(x => x.CreateAt.HasValue && x.Fllow_Ho_Final > 0 &&
                              x.CreateAt.Value.Date >= fromDate.Value.Date &&
                              x.CreateAt.Value.Date <= toDate.Value.Date)
                    //only take 1 record per day, dont check time
                    .GroupBy(x => x.CreateAt.Value.Date)
                    .Select(x => new FT03DataPoint
                    {
                        Date = x.Key,
                        Value = x.First().Fllow_Ho_Final
                    })
                    .OrderBy(x => x.Date)
                    .ToListAsync();

                return await Result<List<FT03DataPoint>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<List<FT03DataPoint>>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }
        public async Task<Result<FT03>> GetByIdAsync([Path] Guid id)
        {
            try
            {
                return await Result<FT03>.SuccessAsync(await dbContext.FT03s.FindAsync(id));
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT03>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<FT03>> InsertAsync([Body] FT03 model)
        {
            try
            {
                //lay thong tin user
                var userInfo = contextAccessor.HttpContext.User.FindFirst("UserId");

                model.CreateAt = DateTime.Now;
                model.CreateOperatorId = userInfo.ToString();
                model.IsDeleted = false;

                await dbContext.FT03s.AddAsync(model);
                await dbContext.SaveChangesAsync();
                return await Result<FT03>.SuccessAsync(model);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT03>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<FT03>> UpdateAsync([Body] FT03 model)
        {
            try
            {
                //lay thong tin user
                var userInfo = contextAccessor.HttpContext.User.FindFirst("UserId");

                model.UpdateAt = DateTime.Now;
                model.UpdateOperatorId = userInfo.ToString();

                dbContext.FT03s.Update(model);
                await dbContext.SaveChangesAsync();
                return await Result<FT03>.SuccessAsync(model);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT03>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }
    }
}
