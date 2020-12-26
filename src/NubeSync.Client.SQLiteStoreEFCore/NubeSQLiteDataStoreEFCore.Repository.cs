﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NubeSync.Core;

namespace NubeSync.Client.SQLiteStoreEFCore
{
    public partial class NubeSQLiteDataStoreEFCore
    {
        public Task<IQueryable<T>> AllAsync<T>() where T : NubeTable
        {
            return Task.FromResult(Set<T>().AsNoTracking().AsQueryable());
        }

        public async Task<bool> DeleteAsync<T>(T item) where T : NubeTable
        {
            var dbItem = await Set<T>().FindAsync(item.Id).ConfigureAwait(false);
            Set<T>().Remove(dbItem);
            return await SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public Task<IQueryable<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate) where T : NubeTable
        {
            return Task.FromResult(Set<T>().AsNoTracking().Where(predicate));
        }

        public async Task<T> FindByIdAsync<T>(string? id) where T : NubeTable?
        {
            var entity = await Set<T>().FindAsync(id).ConfigureAwait(false);
            if (entity != null)
            {
                Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public async Task<bool> InsertAsync<T>(T item) where T : NubeTable
        {
            await Set<T>().AddAsync(item).ConfigureAwait(false);
            return await SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public Task<bool> TableExistsAsync<T>() where T : NubeTable
        {
            try
            {
                Set<T>().Count();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateAsync<T>(T item) where T : NubeTable
        {
            Set<T>().Attach(item);
            Entry(item).State = EntityState.Modified;
            return await SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}