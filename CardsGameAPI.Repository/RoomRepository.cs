using CardsGameAPI.Data;
using CardsGameAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CardsGameAPI.Repository
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {        
        public RoomRepository(Contexts context) : base(context)
        {
        }

        public async Task<IList<Room>> ListCardsAsync()
        {
            try
            {
                return await context.Set<Room>().Include(i => i.DeckCards).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(ListAll)} error: {ex.Message}");
            }
        }

    }
}
