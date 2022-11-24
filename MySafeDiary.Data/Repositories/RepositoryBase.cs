using Microsoft.EntityFrameworkCore;
using MySafeDiary.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MySafeDiary.Data.Repositories
{
    public abstract class RepositoryBase<T> :ContextInitialisator, IRepositoryBase<T> where T : class
    {
        public IQueryable<T> FindAll()
        {
            return BotContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return BotContext.Set<T>()
                .Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            BotContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            BotContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            BotContext.Set<T>().Remove(entity);
        }
        public async Task SaveAsync()
        {
            await BotContext.SaveChangesAsync();
        }
    }
}
