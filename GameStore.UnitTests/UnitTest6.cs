using System.Collections.Generic;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using Moq;
using NUnit.Framework;

namespace GameStore.UnitTests
{
    public class UnitTest6
    {
        [Test]
        public void Indicates_Selected_Category()
        {
            //org
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game {GameId = 1, Name = "Игра1", Category = "Симулятор"},
                new Game {GameId = 2, Name = "Игра2", Category = "Шутер"}
            });
            NavController cont = new NavController(mock.Object);
            
            //action
            var result = (string)cont.Menu("Симулятор").ViewBag.SelectedCategory;

            //assert
            Assert.AreEqual(result, "Симулятор");
        }
    }
}