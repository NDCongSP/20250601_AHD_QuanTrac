using Application.DTOs.Request.Account;
using Application.Extentions;
using Application.Services;
using Application.Services.Base;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryScadaUserServices(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IRepository<Guid, ScadaUser>, IScadaUser
{
    public async Task<Result<List<ScadaUser>>> AddRangeAsync(List<ScadaUser> model)
    {
        try
        {
            await dbContext.ScadaUsers.AddRangeAsync(model);
            await dbContext.SaveChangesAsync();
            return Result<List<ScadaUser>>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<List<ScadaUser>>.Fail($"Failed to add ScadaUser: {ex.Message}");
        }
    }

    public async Task<Result<ScadaUser>> DeleteAsync(ScadaUser model)
    {
        try
        {
            dbContext.ScadaUsers.Remove(model);
            await dbContext.SaveChangesAsync();
            return await Result<ScadaUser>.SuccessAsync(model);
        }
        catch (Exception ex)
        {
            return await Result<ScadaUser>.FailAsync($"Failed to delete ScadaUser: {ex.Message}");
        }
    }

    public async Task<Result<ScadaUser>> DeleteRangeAsync(List<ScadaUser> model)
    {
        try
        {
            dbContext.ScadaUsers.RemoveRange(model);
            await dbContext.SaveChangesAsync();
            return await Result<ScadaUser>.SuccessAsync("");
        }
        catch (Exception ex)
        {
            return await Result<ScadaUser>.FailAsync($"Failed to delete ScadaUser: {ex.Message}");
        }
    }

    public async Task<Result<List<ScadaUser>>> GetAllAsync()
    {
        try
        {
            var items = await dbContext.ScadaUsers
                .OrderBy(x => x.UserName)
                .ToListAsync();
            return Result<List<ScadaUser>>.Success(items);
        }
        catch (Exception ex)
        {
            return Result<List<ScadaUser>>.Fail($"Failed to retrieve ScadaUser: {ex.Message}");
        }
    }

    public async Task<Result<ScadaUser>> GetByIdAsync(Guid id)
    {
        try
        {
            var item = await dbContext.ScadaUsers.FindAsync(id);
            if (item == null)
            {
                return await Result<ScadaUser>.FailAsync("ScadaUser.NotFound");
            }
            return await Result<ScadaUser>.SuccessAsync(item);
        }
        catch (Exception ex)
        {
            return await Result<ScadaUser>.FailAsync($"Failed to retrieve ScadaUser: {ex.Message}");
        }
    }

    public async Task<Result<ScadaUser>> InsertAsync(ScadaUser model)
    {
        try
        {
            model.Password = BCryptHelper.HashPassword(model.Password);
            model.CreateAt = DateTime.Now;
            model.CreateOperatorId = contextAccessor.HttpContext.User.Identity.Name;

            await dbContext.ScadaUsers.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return await Result<ScadaUser>.SuccessAsync(model);
        }
        catch (Exception ex)
        {
            return await Result<ScadaUser>.FailAsync($"Failed to insert ScadaUser: {ex.Message}");
        }
    }

    public async Task<Result<ScadaUser>> UpdateAsync(ScadaUser model)
    {
        try
        {
            var oldPassword = await dbContext.ScadaUsers.Where(x => x.Id == model.Id).Select(x => x.Password).FirstOrDefaultAsync();
            if (oldPassword == null)
            {
                return await Result<ScadaUser>.FailAsync("ScadaUser.OldPasswordNotFound");
            }
            var existingItem = await dbContext.ScadaUsers.FindAsync(model.Id);
            if (existingItem == null)
            {
                return await Result<ScadaUser>.FailAsync("ScadaUser.NotFound");
            }
            model.Password = oldPassword;

            dbContext.Entry(existingItem).CurrentValues.SetValues(model);
            dbContext.Entry(existingItem).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();

            return await Result<ScadaUser>.SuccessAsync(existingItem);
        }
        catch (Exception ex)
        {
            return await Result<ScadaUser>.FailAsync($"Failed to update ScadaUser: {ex.Message}");
        }
    }

    public async Task<Result<ScadaUser>> ChangePasswordAsync(ChangePassRequestDTO model)
    {
        try
        {
            var user = await dbContext.ScadaUsers.FindAsync(Guid.Parse(model.Id));
            if (user == null)
            {
                return await Result<ScadaUser>.FailAsync("ScadaUser.NotFound");
            }
            if (!BCryptHelper.VerifyPassword(model.CurrentPassword, user.Password))
            {
                return await Result<ScadaUser>.FailAsync("ScadaUser.CurrentPasswordIncorrect");
            }
            user.Password = BCryptHelper.HashPassword(model.NewPassword);
            user.UpdateAt = DateTime.Now;
            user.UpdateOperatorId = contextAccessor.HttpContext.User.Identity.Name;
            await dbContext.SaveChangesAsync();
            return await Result<ScadaUser>.SuccessAsync(user);
        }
        catch (Exception ex)
        {
            return await Result<ScadaUser>.FailAsync($"Failed to change password: {ex.Message}");
        }
    }

    public async Task<Result<bool>> ResetPasswordAsync(Guid id)
    {
        try
        {
            var user = await dbContext.ScadaUsers.FindAsync(id);
            if (user == null)
            {
                return await Result<bool>.FailAsync("ScadaUser.NotFound");
            }

            user.Password = BCryptHelper.HashPassword("123@456");
            user.UpdateAt = DateTime.Now;
            user.UpdateOperatorId = contextAccessor.HttpContext.User.Identity.Name;

            await dbContext.SaveChangesAsync();

            return await Result<bool>.SuccessAsync(true, "Password reset successfully");
        }
        catch (Exception ex)
        {
            return await Result<bool>.FailAsync($"Failed to reset password: {ex.Message}");
        }
    }
}

