using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Interfaces;
using PetProject.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PetProject.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
        {
            return await FindAsync(predicate, asNoTracking, Array.Empty<string>());
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking, params string[] includes)
        {
            IQueryable<T> query = dbSet;
            
            // Apply includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            
            if (asNoTracking)
                query = query.AsNoTracking();
            
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = false)
        {
            return await GetAllAsync(asNoTracking, Array.Empty<string>());
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking, params string[] includes)
        {
            IQueryable<T> query = dbSet;
            
            // Apply includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await GetByIdAsync(id, Array.Empty<string>());
        }

        public async Task<T?> GetByIdAsync(int id, params string[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                return await dbSet.FindAsync(id);
            }
            
            IQueryable<T> query = dbSet;
            
            // Apply includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            
            // Use reflection to find Id property
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "e");
                var property = System.Linq.Expressions.Expression.Property(parameter, idProperty);
                var constant = System.Linq.Expressions.Expression.Constant(id);
                var equals = System.Linq.Expressions.Expression.Equal(property, constant);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(equals, parameter);
                
                return await query.FirstOrDefaultAsync(lambda);
            }
            
            // Fallback to FindAsync if no Id property
            return await dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
