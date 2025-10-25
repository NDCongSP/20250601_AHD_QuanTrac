using Domain;
using Domain.Entities;
using Domain.Models;
using RestEase;
using Application.DTOs.Response;

namespace Application.Services;

[BasePath(ApiRoutes.FT03.BasePath)]
public interface IFT03 : IRepository<Guid, FT03>
{
    [Get(ApiRoutes.FT03.GetByFromDateToDateAsync)]
    Task<Result<List<FT03DataPoint>>> GetByFromDateToDateAsync(DateTime? fromDate = null, DateTime? toDate = null);

    [Get(ApiRoutes.FT03.GetSampled)]
    Task<Result<List<TimeValueResponse>>> GetSampledAsync([Query] string paramName, [Query] int frequency = 10);
}
