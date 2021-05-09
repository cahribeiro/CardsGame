using CardsGameAPI.Controllers;
using CardsGameAPI.Repository;
using CardsGameAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CardsGameAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace CardsGame.Tests
{
    public class DeckCardsControllerTests
    {               

        [Fact]
        public async Task GetDeckCards_Returns_First_Card_Success()
        {
            //Arrange
            var mockRepo = new Mock<IRoomRepository>();
            var controller = new DeckCardsController(mockRepo.Object);
            
            //Act
            var okResult = await controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(okResult);

        }

        
    }
}
