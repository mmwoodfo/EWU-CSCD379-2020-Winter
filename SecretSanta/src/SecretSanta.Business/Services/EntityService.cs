using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    class EntityService<TEntity> : IEntityService<TEntity>
    {
        Task<bool> IEntityService<TEntity>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<TEntity>> IEntityService<TEntity>.FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IEntityService<TEntity>.FetchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IEntityService<TEntity>.InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task<TEntity[]> IEntityService<TEntity>.InsertAsync(params TEntity[] entity)
        {
            throw new NotImplementedException();
        }
    }
}
