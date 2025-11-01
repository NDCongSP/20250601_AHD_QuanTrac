//using Infrastructure.BasicEntity;
//using Infrastructure.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using System.Security.Claims;

//namespace Infrastructure.Interceptors;

//public class AuditableSaveChangesInterceptor: SaveChangesInterceptor
//{
//    private readonly IHttpContextAccessor _httpContextAccessor;
//    public async Task<UserLogin> GetCurrentUser()
//    {
//        if (_httpContextAccessor.HttpContext != null)
//        {
//            var user = _httpContextAccessor.HttpContext.User;
//            if (user != null && user.Claims.Count() > 0)
//            {
//                var result = new UserLogin();
//                result.UserID = Int32.Parse(user.FindFirst("userid").Value);
//                result.FullName = user.FindFirst(ClaimTypes.Name)?.Value;
//                result.Email = user.FindFirst(ClaimTypes.Email)?.Value;
//                result.RoleID = short.Parse(user.FindFirst(ClaimTypes.Role).Value);
//                result.RoleName = user.FindFirst("rolename").Value;
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
//                entry.Entity.CreatedDate = DateTimeOffset.Now;
//            }

//            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
//            {
//                entry.Entity.LastModifiedBy = user.Email;
//                entry.Entity.LastModifiedDate = DateTime.Now;
//            }
//        }

//    }
//}

//public static class Extensions
//{
//    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
//        entry.References.Any(r =>
//            r.TargetEntry != null &&
//            r.TargetEntry.Metadata.IsOwned() &&
//            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
//}
