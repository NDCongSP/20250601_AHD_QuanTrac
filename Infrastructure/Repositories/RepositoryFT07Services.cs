using Application.Extentions;
using Application.Services;
using Application.Services.Base;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryFT07Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IRepository<Guid, FT07_ChartMucNuoc>, IFT07
{
    public async Task<Result<List<FT07_ChartMucNuoc>>> AddRangeAsync(List<FT07_ChartMucNuoc> model)
    {
        try
        {
            await dbContext.FT07s_ChartMucNuoc.AddRangeAsync(model);
            await dbContext.SaveChangesAsync();
            return Result<List<FT07_ChartMucNuoc>>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<List<FT07_ChartMucNuoc>>.Fail($"Failed to add FT05_ChartHoChua: {ex.Message}");
        }
    }

    public async Task<Result<FT07_ChartMucNuoc>> DeleteAsync(FT07_ChartMucNuoc model)
    {
        try
        {
            dbContext.FT07s_ChartMucNuoc.Remove(model);
            await dbContext.SaveChangesAsync();
            return await Result<FT07_ChartMucNuoc>.SuccessAsync(model);
        }
        catch (Exception ex)
        {
            return await Result<FT07_ChartMucNuoc>.FailAsync($"Failed to delete FT07_ChartMucNuoc: {ex.Message}");
        }
    }

    public async Task<Result<FT07_ChartMucNuoc>> DeleteRangeAsync(List<FT07_ChartMucNuoc> model)
    {
        try
        {
            dbContext.FT07s_ChartMucNuoc.RemoveRange(model);
            await dbContext.SaveChangesAsync();
            return await Result<FT07_ChartMucNuoc>.SuccessAsync("");
        }
        catch (Exception ex)
        {
            return await Result<FT07_ChartMucNuoc>.FailAsync($"Failed to delete FT07_ChartMucNuoc: {ex.Message}");
        }
    }

    public async Task<Result<List<FT07_ChartMucNuoc>>> GetAllAsync()
    {
        try
        {
            var items = await dbContext.FT07s_ChartMucNuoc
                .OrderBy(x => x.Index)
                .ToListAsync();
            return Result<List<FT07_ChartMucNuoc>>.Success(items);
        }
        catch (Exception ex)
        {
            return Result<List<FT07_ChartMucNuoc>>.Fail($"Failed to retrieve FT07_ChartMucNuoc: {ex.Message}");
        }
    }

    public async Task<Result<FT07_ChartMucNuoc>> GetByIdAsync(Guid id)
    {
        try
        {
            var item = await dbContext.FT07s_ChartMucNuoc.FindAsync(id);
            if (item == null)
            {
                return await Result<FT07_ChartMucNuoc>.FailAsync("Item not found");
            }
            return await Result<FT07_ChartMucNuoc>.SuccessAsync(item);
        }
        catch (Exception ex)
        {
            return await Result<FT07_ChartMucNuoc>.FailAsync($"Failed to retrieve FT07_ChartMucNuoc: {ex.Message}");
        }
    }

    public async Task<Result<FT07_ChartMucNuoc>> InsertAsync(FT07_ChartMucNuoc model)
    {
        try
        {
            await dbContext.FT07s_ChartMucNuoc.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return await Result<FT07_ChartMucNuoc>.SuccessAsync(model);
        }
        catch (Exception ex)
        {
            return await Result<FT07_ChartMucNuoc>.FailAsync($"Failed to insert FT07_ChartMucNuoc: {ex.Message}");
        }
    }

    public async Task<Result<FT07_ChartMucNuoc>> UpdateAsync(FT07_ChartMucNuoc model)
    {
        try
        {
            var existingItem = await dbContext.FT07s_ChartMucNuoc.FindAsync(model.Id);
            if (existingItem == null)
            {
                return await Result<FT07_ChartMucNuoc>.FailAsync("Item not found");
            }

            // Update all properties except Id and X_Value (since X_Value is read-only)
            dbContext.Entry(existingItem).CurrentValues.SetValues(model);
            
            // Mark the entity as modified
            dbContext.Entry(existingItem).State = EntityState.Modified;
            
            await dbContext.SaveChangesAsync();
            
            return await Result<FT07_ChartMucNuoc>.SuccessAsync(existingItem);
        }
        catch (Exception ex)
        {
            return await Result<FT07_ChartMucNuoc>.FailAsync($"Failed to update FT07_ChartMucNuoc: {ex.Message}");
        }
    }
}

