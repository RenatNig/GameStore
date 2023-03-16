using System.Collections.Generic;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
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
        
        [Test]
        public void Generate_Category_Specific_Game_Count()
        {
            /// Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Игра1", Category="Cat1"},
                new Game { GameId = 2, Name = "Игра2", Category="Cat2"},
                new Game { GameId = 3, Name = "Игра3", Category="Cat1"},
                new Game { GameId = 4, Name = "Игра4", Category="Cat2"},
                new Game { GameId = 5, Name = "Игра5", Category="Cat3"}
            });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((GamesListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((GamesListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((GamesListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((GamesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}