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
using System.Reflection;

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

                // Lấy tất cả dữ liệu trong khoảng ngày
                var allData = await dbContext.FT03s
                    .Where(x => x.CreateAt.HasValue
                        && x.Fllow_Ho_Final > 0
                        && x.CreateAt.Value.Date >= fromDate.Value.Date
                        && x.CreateAt.Value.Date <= toDate.Value.Date)
                    .OrderBy(x => x.CreateAt)
                    .Select(x => new FT03DataPoint
                    {
                        Date = x.CreateAt.Value,
                        Value = x.Fllow_Ho_Final
                    })
                    .ToListAsync();

                // Lấy giá trị đầu và cuối mỗi ngày trên memory
                var data = allData
                    .GroupBy(x => x.Date.Date)
                    .SelectMany<IGrouping<DateTime, FT03DataPoint>, FT03DataPoint>(g =>
                    {
                        var ordered = g.OrderBy(x => x.Date).ToList();
                        if (ordered.Count == 1)
                            return ordered; // Chỉ có 1 record thì lấy luôn
                        return new List<FT03DataPoint> { ordered.First(), ordered.Last() }; // Lấy đầu và cuối
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                return await Result<List<FT03DataPoint>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<List<FT03DataPoint>>.FailAsync(JsonConvert.SerializeObject(err));
            }
        }

        public async Task<Result<List<TimeValueResponse>>> GetSampledAsync(string paramName, int frequency = 10)
        {
            try
            {
                if (frequency <= 0) frequency = 10;

                var prop = typeof(FT03).GetProperty(paramName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (prop == null || (prop.PropertyType != typeof(double?) && prop.PropertyType != typeof(double)))
                {
                    return await Result<List<TimeValueResponse>>.FailAsync($"Invalid paramName: {paramName}");
                }

                // Lấy dữ liệu trong 24h đến thời điểm hiện tại
                var now = DateTime.Now;
                var from = now.AddDays(-1);
                var to = now;

                var items = await dbContext.FT03s.AsNoTracking()
                    .Where(x => 
                        x.CreateAt.HasValue && x.CreateAt.Value >= from && x.CreateAt.Value <= to
                        && x.StationId == 4
                    )
                    .Select(x => new { x.CreateAt, Value = (double?)prop.GetValue(x) })
                    .ToListAsync();

                // Gom theo phút, lấy record mới nhất mỗi phút
                var perMinuteLatest = items
                    .Select(r => new
                    {
                        Timestamp = r.CreateAt!.Value,
                        Minute = new DateTime(r.CreateAt.Value.Year, r.CreateAt.Value.Month, r.CreateAt.Value.Day, r.CreateAt.Value.Hour, r.CreateAt.Value.Minute, 0),
                        r.Value
                    })
                    .Where(t => t.Value.HasValue)
                    .GroupBy(t => t.Minute)
                    .Select(g => g.OrderByDescending(z => z.Timestamp).First())
                    .OrderBy(t => t.Timestamp)
                    .ToList();

                if (perMinuteLatest.Count == 0)
                {
                    return await Result<List<TimeValueResponse>>.SuccessAsync(new List<TimeValueResponse>());
                }

                var startMinute = perMinuteLatest.First().Minute;
                var sampled = perMinuteLatest
                    .Where(t => ((int)(t.Minute - startMinute).TotalMinutes % frequency) == 0)
                    .OrderBy(t => t.Timestamp)
                    .Select(t => new TimeValueResponse
                    {
                        CreatedAt = t.Timestamp,
                        Value = t.Value.HasValue ? Math.Round(t.Value.Value, 2) : null
                    })
                    .ToList();

                return await Result<List<TimeValueResponse>>.SuccessAsync(sampled);
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse();
                err.Errors.Add("Error", $"{ex.Message} | {ex.InnerException}");
                return await Result<List<TimeValueResponse>>.FailAsync(JsonConvert.SerializeObject(err));
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
