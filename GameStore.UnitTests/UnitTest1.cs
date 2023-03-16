using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;

namespace GameStore.UnitTests
{
    public class UnitTest1
    {
        [Test]
        public void Can_Paginate()
        {
            //организация
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Игра1"},
                new Game { GameId = 2, Name = "Игра2"},
                new Game { GameId = 3, Name = "Игра3"},
                new Game { GameId = 4, Name = "Игра4"},
                new Game { GameId = 5, Name = "Игра5"}
            });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            //действие
            GamesListViewModel result = (GamesListViewModel)controller.List(null,2).Model;

            //утверждение
            List<Game> games = result.Games.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Игра4");
            Assert.AreEqual(games[1].Name, "Игра5");
        }
    }
}