using CardsGameAPI.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardsGameAPI.Repository.Interface
{
    public interface IRoomRepository : IRepositoryBase<Room>
    {        
        Task<IList<Room>> ListCardsAsync();
    }
}
