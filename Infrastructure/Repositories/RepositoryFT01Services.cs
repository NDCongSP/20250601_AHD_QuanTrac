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
    public class RepositoryFT01Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IFT01
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

        public async Task<Result<bool>> DeleteLocationAsync(int? locationId)
        {
            try
            {
                var ft01 = await dbContext.FT01s.FirstOrDefaultAsync();
                if (ft01 == null)
                {
                    return await Result<bool>.FailAsync("No FT01 record found");
                }

                var locations = await GetLocationsFromC001(ft01);

                var locationToRemove = locations.FirstOrDefault(x => x.Id == locationId);
                if (locationToRemove != null)
                {
                    locations.Remove(locationToRemove);

                    await SaveLocationsToC001(ft01, locations);

                    return await Result<bool>.SuccessAsync(true);
                }

                return await Result<bool>.FailAsync("Location not found");
            }
            catch (Exception ex)
            {
                return await Result<bool>.FailAsync(ex.Message);
            }
        }

        public async Task<Result<LocationInfoModel>> AddOrUpdateLocationAsync(LocationInfoModel location)
        {
            try
            {
                var ft01 = await dbContext.FT01s.FirstOrDefaultAsync();
                if (ft01 == null)
                {
                    ft01 = new FT01
                    {
                        Id = Guid.NewGuid(),
                        C001 = "[]"
                    };
                    await dbContext.FT01s.AddAsync(ft01);
                    await dbContext.SaveChangesAsync();
                }
                var locations = await GetLocationsFromC001(ft01);

                if (location.Id == null || location.Id == 0)
                {
                    location.Id = locations.Any() ? locations.Max(x => x.Id) + 1 : 1;
                    locations.Add(location);
                }
                else
                {
                    var existingLocation = locations.FirstOrDefault(x => x.Id == location.Id);
                    if (existingLocation != null)
                    {
                        var index = locations.IndexOf(existingLocation);
                        locations[index] = location;
                    }
                    else
                    {
                        locations.Add(location);
                    }
                }

                await SaveLocationsToC001(ft01, locations);

                return await Result<LocationInfoModel>.SuccessAsync(location);
            }
            catch (Exception ex)
            {
                return HandleError<LocationInfoModel>(ex);
            }
        }

        public async Task<Result<ConfigModel>> GetConfigAsync()
        {
            try
            {
                var ft01 = await dbContext.FT01s.FirstOrDefaultAsync();
                if (ft01 == null)
                {
                    // Return default config if no record exists
                    return await Result<ConfigModel>.SuccessAsync(new ConfigModel());
                }

                if (string.IsNullOrEmpty(ft01.C000))
                {
                    return await Result<ConfigModel>.SuccessAsync(new ConfigModel());
                }

                var config = JsonConvert.DeserializeObject<ConfigModel>(ft01.C000);
                return await Result<ConfigModel>.SuccessAsync(config);
            }
            catch (Exception ex)
            {
                return HandleError<ConfigModel>(ex);
            }
        }

        public async Task<Result<ConfigModel>> UpdateConfigAsync(ConfigModel config)
        {
            try
            {
                var ft01 = await dbContext.FT01s.FirstOrDefaultAsync();
                if (ft01 == null)
                {
                    ft01 = new FT01
                    {
                        Id = Guid.NewGuid(),
                        C000 = JsonConvert.SerializeObject(config)
                    };
                    await dbContext.FT01s.AddAsync(ft01);
                }
                else
                {
                    ft01.C000 = JsonConvert.SerializeObject(config);
                    dbContext.FT01s.Update(ft01);
                }

                await dbContext.SaveChangesAsync();
                return await Result<ConfigModel>.SuccessAsync(config);
            }
            catch (Exception ex)
            {
                return HandleError<ConfigModel>(ex);
            }
        }

        private async Task<List<LocationInfoModel>> GetLocationsFromC001(FT01 ft01)
        {
            return string.IsNullOrEmpty(ft01.C001)
                ? new List<LocationInfoModel>()
                : JsonConvert.DeserializeObject<List<LocationInfoModel>>(ft01.C001) ?? new List<LocationInfoModel>();
        }

        private async Task SaveLocationsToC001(FT01 ft01, List<LocationInfoModel> locations)
        {
            try
            {
                ft01.C001 = JsonConvert.SerializeObject(locations);

                if (ft01.Id == null || ft01.Id == Guid.Empty)
                {
                    await dbContext.FT01s.AddAsync(ft01);
                }
                else
                {
                    dbContext.FT01s.Update(ft01);
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
