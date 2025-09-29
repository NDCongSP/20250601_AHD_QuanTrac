using Application.Extentions;
using Application.Services;
using Application.Services.Base;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryFT05Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IRepository<Guid, FT05_ChartHoChua>, IFT05
{
    public async Task<Result<List<FT05_ChartHoChua>>> AddRangeAsync(List<FT05_ChartHoChua> model)
    {
        try
        {
            await dbContext.FT05s_ChartHoChua.AddRangeAsync(model);
            await dbContext.SaveChangesAsync();
            return Result<List<FT05_ChartHoChua>>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<List<FT05_ChartHoChua>>.Fail($"Failed to add FT05_ChartHoChua: {ex.Message}");
        }
    }

    public async Task<Result<FT05_ChartHoChua>> DeleteAsync(FT05_ChartHoChua model)
    {
        try
        {
            dbContext.FT05s_ChartHoChua.Remove(model);
            await dbContext.SaveChangesAsync();
            return Result<FT05_ChartHoChua>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<FT05_ChartHoChua>.Fail($"Failed to delete FT05_ChartHoChua: {ex.Message}");
        }
    }

    public async Task<Result<FT05_ChartHoChua>> DeleteRangeAsync(List<FT05_ChartHoChua> model)
    {
        try
        {
            dbContext.FT05s_ChartHoChua.RemoveRange(model);
            await dbContext.SaveChangesAsync();
            return Result<FT05_ChartHoChua>.Success("");
        }
        catch (Exception ex)
        {
            return Result<FT05_ChartHoChua>.Fail($"Failed to delete FT05_ChartHoChua: {ex.Message}");
        }
    }

    public async Task<Result<List<FT05_ChartHoChua>>> GetAllAsync()
    {
        try
        {
            var items = await dbContext.FT05s_ChartHoChua
                .OrderBy(x => x.Index)
                .ToListAsync();
            return Result<List<FT05_ChartHoChua>>.Success(items);
        }
        catch (Exception ex)
        {
            return Result<List<FT05_ChartHoChua>>.Fail($"Failed to retrieve FT05_ChartHoChua: {ex.Message}");
        }
    }

    public async Task<Result<FT05_ChartHoChua>> GetByIdAsync(Guid id)
    {
        try
        {
            var item = await dbContext.FT05s_ChartHoChua.FindAsync(id);
            if (item == null)
            {
                return Result<FT05_ChartHoChua>.Fail("Item not found");
            }
            return Result<FT05_ChartHoChua>.Success(item);
        }
        catch (Exception ex)
        {
            return Result<FT05_ChartHoChua>.Fail($"Failed to retrieve FT05_ChartHoChua: {ex.Message}");
        }
    }

    public async Task<Result<FT05_ChartHoChua>> InsertAsync(FT05_ChartHoChua model)
    {
        try
        {
            await dbContext.FT05s_ChartHoChua.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return Result<FT05_ChartHoChua>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<FT05_ChartHoChua>.Fail($"Failed to insert FT05_ChartHoChua: {ex.Message}");
        }
    }

    public async Task<Result<FT05_ChartHoChua>> UpdateAsync(FT05_ChartHoChua model)
    {
        try
        {
            var existingItem = await dbContext.FT05s_ChartHoChua.FindAsync(model.Id);
            if (existingItem == null)
            {
                return Result<FT05_ChartHoChua>.Fail("Item not found");
            }

            // Update all properties except Id and X_Value (since X_Value is read-only)
            dbContext.Entry(existingItem).CurrentValues.SetValues(model);
            
            // Mark the entity as modified
            dbContext.Entry(existingItem).State = EntityState.Modified;
            
            await dbContext.SaveChangesAsync();
            
            return Result<FT05_ChartHoChua>.Success(existingItem);
        }
        catch (Exception ex)
        {
            return Result<FT05_ChartHoChua>.Fail($"Failed to update FT05_ChartHoChua: {ex.Message}");
        }
    }
}

