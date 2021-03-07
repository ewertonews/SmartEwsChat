using EwsChat.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EwsChat.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ChatContext ChatContext { get; set; }
        public RepositoryBase(ChatContext chatContext)
        {
            ChatContext = chatContext;
        }
        public IQueryable<T> FindAll()
        {
            return ChatContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return ChatContext.Set<T>()
                .Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            ChatContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            ChatContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            ChatContext.Set<T>().Remove(entity);
        }       
    }
}
