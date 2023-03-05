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
using GameStore.WebUI.HtmlHelpers;

namespace GameStore.UnitTests
{
    public class UnitTest2
    {
        [Test]
        public void Can_Generate_Page_Links()
        {
            //организация
            HtmlHelper htmlHelper = null;
            
            PagingInfo pagingInfo = new PagingInfo()
            {
                CurrentPage = 2,
                ItemsPerPage = 4,
                TotalItems = 10
            };

            string someFunc(int i)
            {
                return "Page" + i;
            }
            
            Func<int, string> pageUrlDelegate = new Func<int, string>(someFunc);
            
            //действие
            var result = PagingHelpers.PageLinks(htmlHelper, pagingInfo, pageUrlDelegate);
            //утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());

        }
    }
}
