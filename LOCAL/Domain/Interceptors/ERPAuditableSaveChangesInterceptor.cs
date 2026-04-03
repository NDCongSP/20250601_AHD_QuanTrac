//using Infrastructure.BasicEntity;
//using Infrastructure.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using System.Security.Claims;

//namespace Infrastructure.Interceptors;

//public class ERPAuditableSaveChangesInterceptor : SaveChangesInterceptor
//{
//    private readonly IHttpContextAccessor _httpContextAccessor;
//    public ERPAuditableSaveChangesInterceptor(IHttpContextAccessor httpContextAccessor)
//    {
//        _httpContextAccessor = httpContextAccessor;
//    }

//    public async Task<UserLogin> GetCurrentUser()
//    {
//        if (_httpContextAccessor.HttpContext != null)
//        {
//            var user = _httpContextAccessor.HttpContext.User;
//            if (user != null && user.Claims.Count() > 0)
//            {
//                var result = new UserLogin();
//                result.Email = user.FindFirst(ClaimTypes.Email) == null ? "depvelop" : user.FindFirst(ClaimTypes.Email).Value;
//                return result;
//            }
//        }

//        return new UserLogin();
//    }

//    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//    {
//        UpdateEntities(eventData.Context);

//        return base.SavingChanges(eventData, result);
//    }

//    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
//    {
//        UpdateEntities(eventData.Context);

//        return base.SavingChangesAsync(eventData, result, cancellationToken);
//    }

//    public async Task UpdateEntities(DbContext? context)
//    {
//        if (context == null) return;

//        var user = await GetCurrentUser();

//        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
//        {
//            if (entry.State == EntityState.Added)
//            {
//                entry.Entity.CreatedBy = user.Email;
//                entry.Entity.CreatedDate = DateTime.Now;
//            }

//            if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
//            {
//                entry.Entity.LastModifiedDate = DateTime.Now;
//                entry.Entity.LastModifiedBy = user.Email;
//            }
//        }

//        foreach (var entry in context.ChangeTracker.Entries<BasicCreateEntity>())
//        {
//            if (entry.State == EntityState.Added)
//            {
//                entry.Entity.CreatedBy = user.Email;
//                entry.Entity.CreatedDate = DateTime.Now;
//            }
//        }
//    }
//}