using Application.DTOs.Response;
using Application.Extentions;
using Application.Services;
using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestEase;

namespace Infrastructure.Repositories
{
    public class RepositoryFT02Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IFT02
    {
        public Task<Result<List<FT02>>> AddRangeAsync([Body] List<FT02> model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT02>> DeleteAsync([Body] FT02 model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FT02>> DeleteRangeAsync([Body] List<FT02> model)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<FT02>>> GetAllAsync()
        {
            try
            {
                return await Result<List<FT02>>.SuccessAsync(await dbContext.FT02s.ToListAsync());
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<List<FT02>>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<FT02>> GetByIdAsync([Path] Guid id)
        {
            try
            {
                return await Result<FT02>.SuccessAsync(await dbContext.FT02s.FindAsync(id));
            }   
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT02>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<FT02>> InsertAsync([Body] FT02 model)
        {
            try
            {
                //lay thong tin user
                var userInfo = contextAccessor.HttpContext.User.FindFirst("UserId");

                model.CreateAt = DateTime.Now;
                model.CreateOperatorId = userInfo.ToString();
                model.IsDeleted = false;

                await dbContext.FT02s.AddAsync(model);
                await dbContext.SaveChangesAsync();
                return await Result<FT02>.SuccessAsync(model);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT02>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<FT02>> UpdateAsync([Body] FT02 model)
        {
            try
            {
                //lay thong tin user
                var userInfo = contextAccessor.HttpContext.User.FindFirst("UserId");

                model.UpdateAt = DateTime.Now;
                model.UpdateOperatorId = userInfo.ToString();

                dbContext.FT02s.Update(model);
                await dbContext.SaveChangesAsync();
                return await Result<FT02>.SuccessAsync(model);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<FT02>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        private RealtimeDisplayModel GetRealTimeDisplayFromC001(FT02 FT02)
        {
            var result = string.IsNullOrEmpty(FT02.C000)
                ? new List<RealtimeDisplayModel>()
                : JsonConvert.DeserializeObject<List<RealtimeDisplayModel>>(FT02.C000) ?? new List<RealtimeDisplayModel>();
            return result.FirstOrDefault() ?? new RealtimeDisplayModel();
        }

        public async Task<RealtimeDisplayModel> GetFirstOrDefaultRealTimeDisplay()
        {
            try
            {
                var entity = await dbContext.FT02s.FirstOrDefaultAsync();
                if (entity == null)
                {
                    return new RealtimeDisplayModel();
                }
                return GetRealTimeDisplayFromC001(entity);
            }
            catch (Exception)
            {
                // Log the exception if needed
                return new RealtimeDisplayModel();
            }
        }

        private async Task SaveRealTimeDisplayToC000(FT02 FT02, List<RealtimeDisplayModel> locations)
        {
            try
            {
                FT02.C000 = JsonConvert.SerializeObject(locations);

                if (FT02.Id == null || FT02.Id == Guid.Empty)
                {
                    await dbContext.FT02s.AddAsync(FT02);
                }
                else
                {
                    dbContext.FT02s.Update(FT02);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
        
        private Result<T> HandleError<T>(Exception ex) where T : class
        {
            var err = new ErrorResponse();
            err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
            return Result<T>.Fail(JsonConvert.SerializeObject(err));
        }
    }
}
