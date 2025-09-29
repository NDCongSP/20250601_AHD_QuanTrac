using Application.Extentions;
using Application.Services;
using Application.Services.Base;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryFT06Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IRepository<Guid, FT06_InterpolationTable>, IFT06
{
    public async Task<Result<List<FT06_InterpolationTable>>> AddRangeAsync(List<FT06_InterpolationTable> model)
    {
        try
        {
            await dbContext.FT06s_Interpolation.AddRangeAsync(model);
            await dbContext.SaveChangesAsync();
            return Result<List<FT06_InterpolationTable>>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<List<FT06_InterpolationTable>>.Fail($"Failed to add FT05_ChartHoChua: {ex.Message}");
        }
    }

    public async Task<Result<FT06_InterpolationTable>> DeleteAsync(FT06_InterpolationTable model)
    {
        try
        {
            dbContext.FT06s_Interpolation.Remove(model);
            await dbContext.SaveChangesAsync();
            return Result<FT06_InterpolationTable>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<FT06_InterpolationTable>.Fail($"Failed to delete FT06_InterpolationTable: {ex.Message}");
        }
    }

    public async Task<Result<FT06_InterpolationTable>> DeleteRangeAsync(List<FT06_InterpolationTable> model)
    {
        try
        {
            dbContext.FT06s_Interpolation.RemoveRange(model);
            await dbContext.SaveChangesAsync();
            return Result<FT06_InterpolationTable>.Success("");
        }
        catch (Exception ex)
        {
            return Result<FT06_InterpolationTable>.Fail($"Failed to delete FT06_InterpolationTable: {ex.Message}");
        }
    }

    public async Task<Result<List<FT06_InterpolationTable>>> GetAllAsync()
    {
        try
        {
            var items = await dbContext.FT06s_Interpolation
                .OrderBy(x => x.Z)
                .ToListAsync();
            return Result<List<FT06_InterpolationTable>>.Success(items);
        }
        catch (Exception ex)
        {
            return Result<List<FT06_InterpolationTable>>.Fail($"Failed to retrieve FT06_InterpolationTable: {ex.Message}");
        }
    }

    public async Task<Result<FT06_InterpolationTable>> GetByIdAsync(Guid id)
    {
        try
        {
            var item = await dbContext.FT06s_Interpolation.FindAsync(id);
            if (item == null)
            {
                return Result<FT06_InterpolationTable>.Fail("Item not found");
            }
            return Result<FT06_InterpolationTable>.Success(item);
        }
        catch (Exception ex)
        {
            return Result<FT06_InterpolationTable>.Fail($"Failed to retrieve FT06_InterpolationTable: {ex.Message}");
        }
    }

    public async Task<Result<FT06_InterpolationTable>> InsertAsync(FT06_InterpolationTable model)
    {
        try
        {
            await dbContext.FT06s_Interpolation.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return Result<FT06_InterpolationTable>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<FT06_InterpolationTable>.Fail($"Failed to insert FT06_InterpolationTable: {ex.Message}");
        }
    }

    public async Task<Result<FT06_InterpolationTable>> UpdateAsync(FT06_InterpolationTable model)
    {
        try
        {
            var existingItem = await dbContext.FT06s_Interpolation.FindAsync(model.Id);
            if (existingItem == null)
            {
                return Result<FT06_InterpolationTable>.Fail("Item not found");
            }

            // Update all properties except Id and X_Value (since X_Value is read-only)
            dbContext.Entry(existingItem).CurrentValues.SetValues(model);
            
            // Mark the entity as modified
            dbContext.Entry(existingItem).State = EntityState.Modified;
            
            await dbContext.SaveChangesAsync();
            
            return Result<FT06_InterpolationTable>.Success(existingItem);
        }
        catch (Exception ex)
        {
            return Result<FT06_InterpolationTable>.Fail($"Failed to update FT06_InterpolationTable: {ex.Message}");
        }
    }
}

