using Application.Extentions;
using Application.Services;
using Application.Services.Base;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class RepositoryFT05Services(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor) : IRepository<Guid, FT05_ChartHoChua>, IFT05
{
    Task<Result<List<FT05_ChartHoChua>>> IRepository<Guid, FT05_ChartHoChua>.AddRangeAsync(List<FT05_ChartHoChua> model)
    {
        throw new NotImplementedException();
    }

    Task<Result<FT05_ChartHoChua>> IRepository<Guid, FT05_ChartHoChua>.DeleteAsync(FT05_ChartHoChua model)
    {
        throw new NotImplementedException();
    }

    Task<Result<FT05_ChartHoChua>> IRepository<Guid, FT05_ChartHoChua>.DeleteRangeAsync(List<FT05_ChartHoChua> model)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<FT05_ChartHoChua>>> GetAllAsync()
    {
        try
        {
            var items = await dbContext.FT05s_ChartHoChua
                .OrderBy(x => x.X_Value)
                .ToListAsync();
            return Result<List<FT05_ChartHoChua>>.Success(items);
        }
        catch (Exception ex)
        {
            return Result<List<FT05_ChartHoChua>>.Fail($"Failed to retrieve FT05_ChartHoChua: {ex.Message}");
        }
    }

    Task<Result<FT05_ChartHoChua>> IRepository<Guid, FT05_ChartHoChua>.GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<Result<FT05_ChartHoChua>> IRepository<Guid, FT05_ChartHoChua>.InsertAsync(FT05_ChartHoChua model)
    {
        throw new NotImplementedException();
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

