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
    public class CardController : ControllerBase
    {
        private IRoomRepository roomRepository;
        public CardController(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            //get card already in table by id
            try
            {
                if (Id == null)
                {
                    return BadRequest("Id is required.");
                }
                Room room = await GetRoombyId(Id);
                if(room == null)
                {
                    return BadRequest("Id not found");
                }

                Cards actualCard = room.DeckCards[room.CardsPlayed - 1];

                return Ok(new { cardNumber = actualCard.ToString() }) ;

            }
            catch(Exception)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PlayCard([FromBody]CardRequest cardRequest)
        {

            try
            {                
                if(cardRequest.Id == null)
                {
                    return BadRequest("Id is required.");
                }

                CardResponse response = new CardResponse();
                string result = string.Empty;
                Cards actualCard;
                Cards nextCard;
                //get card in table
                Room room = await GetRoombyId(cardRequest.Id);

                if (room == null)
                {
                    return BadRequest("Id not found");
                }

                int cardsPlayed = room.CardsPlayed;

                if(cardsPlayed == 52)
                {
                    //game ended
                    response.result = "Game Ended";
                    return Ok(response);
                }
                else
                {                    
                    actualCard = room.DeckCards[room.CardsPlayed-1];
                    nextCard = room.DeckCards[room.CardsPlayed];
                    int kindIndex = (int)actualCard.Kind;
                    int suitIndex = (int)actualCard.Suit;
                    int kindIndexNext = (int)nextCard.Kind;
                    int suitIndexNext = (int)nextCard.Suit;

                    //check if player wins if guest Higher or Lower
                    if ((cardRequest.isHigher && kindIndexNext >= kindIndex) || (!cardRequest.isHigher && kindIndexNext <= kindIndex) )
                    {
                        result = "You win";
                    }                    
                    else
                    {
                        result = "You loose";
                    }

                    //update cards played number
                    room.CardsPlayed = cardsPlayed + 1;
                    
                    await roomRepository.UpdateAsync(room);

                    response.actualCard = actualCard.ToString();
                    response.nextCard = nextCard.ToString();
                    response.result = result;

                    return Ok(response);
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }


        }        

        private async Task<Room> GetRoombyId(Guid Id)
        {
            try
            {                
                var rooms = await roomRepository.ListCardsAsync();
                var room = rooms.Where(w => w.ID == Id).FirstOrDefault();

                return room;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}
