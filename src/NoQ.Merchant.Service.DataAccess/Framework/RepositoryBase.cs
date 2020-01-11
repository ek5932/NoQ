using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoQ.Framework.Extensions;
using NoQ.Merchant.Service.DataAccess.Entities;

namespace NoQ.Merchant.Service.DataAccess.Framework
{
    public abstract class RepositoryBase<TContext, TEntity> where TEntity : PersistedEntity, new()
    {
        public RepositoryBase(TContext context, Func<TContext, DbSet<TEntity>> entity)
        {
            Context = context;
            Entity = entity(context);
        }

        protected TContext Context { get; }
        protected DbSet<TEntity> Entity { get; }

        protected Task<TEntity[]> GetAll() => Entity.ToArrayAsync();
        protected Task<TEntity> GetById(int id)
        {
            id.VerifyNotZero();
            return Entity.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
