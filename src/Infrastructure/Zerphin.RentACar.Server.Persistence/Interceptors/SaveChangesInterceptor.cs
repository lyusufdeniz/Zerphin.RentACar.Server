using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zerphin.RentACar.Server.Domain.Abstractions;
using Zerphin.RentACar.Server.Persistence.Context;

namespace Zerphin.RentACar.Server.Persistence.Interceptors;

internal static class SaveChangesInterceptor
{
    public static Task<int> SaveChangesAsync(AppDbContext context, CancellationToken cancellationToken)
    {
        var entries = context.ChangeTracker.Entries<Entity>();
        HttpContextAccessor accessor = new HttpContextAccessor();
        var userIdString = accessor.HttpContext!.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userId = Guid.Parse(userIdString);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = userId;
                entry.Entity.IsDeleted = false;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTime.UtcNow;
                entry.Entity.DeletedBy = userId;
                entry.State = EntityState.Modified;
            }
        }
        return context.SaveChangesAsync(cancellationToken);
    }
public static int SaveChanges(AppDbContext context)
    {
        var entries = context.ChangeTracker.Entries<Entity>();
        HttpContextAccessor accessor = new HttpContextAccessor();
        var userIdString = accessor.HttpContext!.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userId = Guid.Parse(userIdString);
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = userId;
                entry.Entity.IsDeleted = false;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTime.UtcNow;
                entry.Entity.DeletedBy = userId;
                entry.State = EntityState.Modified;
            }
        }
        return context.SaveChanges();
    }
}