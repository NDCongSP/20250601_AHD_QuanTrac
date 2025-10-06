using Application.DTOs.Response;
using Domain;
using Domain.Entities;
using Domain.Models;
using RestEase;

namespace Application.Services;

[BasePath(ApiRoutes.FT03.BasePath)]
public interface IFT03 : IRepository<Guid, FT03>
{
    [Get(ApiRoutes.FT03.GetByFromDateToDateAsync)]
    Task<Result<List<FT03DataPoint>>> GetByFromDateToDateAsync(DateTime? fromDate = null, DateTime? toDate = null);
}
