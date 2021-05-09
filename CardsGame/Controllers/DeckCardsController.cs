using CardsGameAPI.Models;
using CardsGameAPI.Data;
using CardsGameAPI.Repository;
using CardsGameAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CardsGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeckCardsController : ControllerBase
    {
        private IRepositoryBase<Room> roomRepository;
        public DeckCardsController(IRepositoryBase<Room> roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {

            try
            {
                List<Cards> deckCards = new List<Cards>();
                //get all cards
                foreach (Cards.Suits suit in Enum.GetValues(typeof(Cards.Suits)))
                {
                    foreach (Cards.Kinds kind in Enum.GetValues(typeof(Cards.Kinds)))
                    {
                        deckCards.Add(new Cards(suit, kind));
                    }
                }

                //shuffle cards
                List<Cards> deckCardsShuffle = Shuffle(deckCards);
                Cards firstCard = deckCardsShuffle.FirstOrDefault();
                Room room = new Room()
                {
                    ID = Guid.NewGuid(),
                    CardsPlayed = 1,
                    DeckCards = deckCardsShuffle
                };                              

                await roomRepository.AddAsync(room);

                DeckCardsResponse response = new DeckCardsResponse()
                {
                    ID = room.ID,
                    cardNumber = firstCard.ToString()
                };

                return Ok(response);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }


        }
       

        private List<Cards> Shuffle(List<Cards> deckCards)
        {
            //shuffle cards
            try
            {
                var random = new Random();
                int countCards = deckCards.Count;

                for (int i = 0; i < (countCards - 1); i++)
                {                    
                    int r = i + random.Next(countCards - i);
                    Cards card = deckCards[r];
                    deckCards[r] = deckCards[i];
                    deckCards[i] = card;
                }

                return deckCards;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
