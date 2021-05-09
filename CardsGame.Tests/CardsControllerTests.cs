using CardsGameAPI.Controllers;
using CardsGameAPI.Data;
using CardsGameAPI.Models;
using CardsGameAPI.Repository;
using CardsGameAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CardsGame.Tests
{
    public class CardsControllerTests
    {
        public Mock<IRoomRepository> mock = new Mock<IRoomRepository>();
        CardController controller;
        DeckCardsController controllerDeck;

        public CardsControllerTests()
        {
            controller = new CardController(mock.Object);
            controllerDeck = new DeckCardsController(mock.Object);
        }

        [Fact]
        public async Task GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            //Arrange
            var guid = Guid.NewGuid();

            // Act
            var notFoundResult = await controller.Get(guid);
            // Assert
            Assert.IsType<BadRequestObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsOkResult()
        {

            // Arrange
            List<Room> listRoom = GetRooms();

            // Act
            mock.Setup(m => m.ListCardsAsync()).ReturnsAsync(listRoom);
            var controllerCard = new CardController(mock.Object);
            var okResult = await controllerCard.Get(listRoom.FirstOrDefault().ID);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

        }

        [Fact]
        public async Task PlayCard_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange

            var cardRequest = new CardRequest()
            {
                isHigher = true

            };
            controller.ModelState.AddModelError("Id", "Required");
            // Act
            var badResponse = await controller.PlayCard(cardRequest);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async Task PlayCard_ValidObjectPassed_ReturnsOkResponse()
        {
            // Arrange
            List<Room> listRoom = GetRooms();

            var cardRequest = new CardRequest()
            {
                Id = listRoom.FirstOrDefault().ID,
                isHigher = true
            };

            // Act
            mock.Setup(m => m.ListCardsAsync()).ReturnsAsync(listRoom);
            var controllerCard = new CardController(mock.Object);
            var okResponse = await controllerCard.PlayCard(cardRequest);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public async Task PlayCard_ValidObjectPassed_ReturnedResponseOkItem()
        {
            // Arrange
            List<Room> listRoom = GetRooms();

            var cardRequest = new CardRequest()
            {
                Id = listRoom.FirstOrDefault().ID,
                isHigher = true
            };

            // Act
            mock.Setup(m => m.ListCardsAsync()).ReturnsAsync(listRoom);
            var controllerCard = new CardController(mock.Object);
            var okResponse = await controllerCard.PlayCard(cardRequest);

            // Assert
            var playResult = Assert.IsType<OkObjectResult>(okResponse);
            var resultPlayValue = (CardResponse)playResult.Value;
            Assert.IsType<CardResponse>(resultPlayValue);
            Assert.True((resultPlayValue.result.Equals("You loose") || resultPlayValue.result.Equals("You win")) ? true : false, "Failed");


        }

        public List<Room> GetRooms()
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
            var random = new Random();
            int countCards = deckCards.Count;

            for (int i = 0; i < (countCards - 1); i++)
            {
                int r = i + random.Next(countCards - i);
                Cards card = deckCards[r];
                deckCards[r] = deckCards[i];
                deckCards[i] = card;
            }

            List<Room> listRoom = new List<Room>();
            Room room = new Room()
            {
                ID = Guid.NewGuid(),
                CardsPlayed = 1,
                DeckCards = deckCards
            };

            listRoom.Add(room);

            return listRoom;
        }

    }
}
