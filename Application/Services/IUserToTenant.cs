using Application.DTOs;
using Application.Extentions;
using Application.Services.Base;
using Domain.Entities;
using RestEase;

namespace Application.Services
{
    [BasePath(ApiRoutes.UserToTenant.BasePath)]
    public interface IUserToTenant : IRepository<Guid, UserToTenant>
    {
        [Get(ApiRoutes.UserToTenant.GetByUserId)]
        Task<Result<List<UserToTenant>>> GetByUserIdAsync([Path]string userId);
        [Get(ApiRoutes.UserToTenant.GetUsersAsync)]
        Task<List<UserDto>> GetUsersAsync();
    }
}