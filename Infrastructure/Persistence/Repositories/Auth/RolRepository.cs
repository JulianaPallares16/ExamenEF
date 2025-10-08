using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Abstractions.Auth;
using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Auth;
public class RolRepository(AppDbContext db) : IRolService
{
    public async Task AddAsync(Rol entity, CancellationToken ct = default)
    {
        db.Rols.Add(entity);
        // await db.SaveChangesAsync(ct);
        await Task.CompletedTask;
    }

    public Task<int> CountAsync(string? search, CancellationToken ct = default)
    {
        var query = db.Rols.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = $"%{search.Trim()}%";
            query = query.Where(p => EF.Functions.ILike(p.Nombre, term));
        }
        return query.CountAsync(ct);
    }

    public IEnumerable<Rol> Find(Expression<Func<Rol, bool>> expression)
    {
        // Global tracking is disabled (UseQueryTrackingBehavior(NoTracking)).
        // For relationship changes we need tracked entities to avoid EF trying
        // to INSERT existing roles (causing duplicate PK errors). Use AsTracking here.
        return db.Set<Rol>().AsTracking().Where(expression);
    }

    public async Task<IEnumerable<Rol>> GetAllAsync(CancellationToken ct = default)
    {
        return await db.Rols.AsNoTracking().ToListAsync(ct);
    }

    public Task<Rol?> GetByIdAsync(int id, CancellationToken ct = default)
        => db.Rols.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);

    public Task<IEnumerable<Rol>> GetPagedAsync(int page, int size, string? q, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Rol entity, CancellationToken ct = default)
    {
        db.Rols.Remove(entity);
        await db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Rol entity, CancellationToken ct = default)
    {
        db.Rols.Update(entity);
        await Task.CompletedTask;
    }
}