﻿using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using SportsStore.Components;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_select_categories()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Products).Returns((new Product[]
            {
                new Product{Name="P1", ProductID=1,Category="Apples"},
                new Product{Name="P2", ProductID=2,Category="Apples"},
                new Product{Name="P3", ProductID=3,Category="Plums"},
                new Product{Name="P4", ProductID=4,Category="Oranges"}
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Act
            string[] result = (((target.Invoke() as ViewViewComponentResult)?
                                .ViewData?.Model as CategoryRouteDataModel)?.Categories
                                ?? Enumerable.Empty<string>()).ToArray();
            //Assert
            Assert.True(Enumerable.SequenceEqual(new string[]
            {
                "Apples",
                "Oranges",
                "Plums"
            }, result));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {

        }
    }
}
