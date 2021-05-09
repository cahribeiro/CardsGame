using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsGameAPI.Repository.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> ListAll();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);   
    }
}
