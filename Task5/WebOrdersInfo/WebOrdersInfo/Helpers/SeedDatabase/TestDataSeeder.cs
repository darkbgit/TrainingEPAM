using System;
using System.Collections.Generic;
using System.Linq;
using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.Helpers.SeedDatabase
{
    public static class TestDataSeeder
    {


        public static void SeedData(WebOrdersInfoContext context)
        {
            context.Database.EnsureCreated();

            if (context.Orders.Any())
            {
                return;
            }

            var clientsIds = Enumerable.Repeat(0, 3)
                .Select(_ => Guid.NewGuid()).ToList();

            var managersIds = Enumerable.Repeat(0, 3)
                .Select(_ => Guid.NewGuid()).ToList();

            var productsIds = Enumerable.Repeat(0, 3)
                .Select(_ => Guid.NewGuid()).ToList();

            var ordersIds = Enumerable.Repeat(0, 20)
                .Select(_ => Guid.NewGuid()).ToList();

            context.Clients.AddRange(
                new Client
                {
                    Id = clientsIds[0],
                    Name = "Client1"
                },
                new Client
                {
                    Id = clientsIds[1],
                    Name = "Client2"
                },
                new Client
                {
                    Id = clientsIds[2],
                    Name = "Client3"
                });
            context.Managers.AddRange(

                new Manager
                {
                    Id = managersIds[0],
                    Name = "Manager1"
                },
                new Manager
                {
                    Id = managersIds[1],
                    Name = "Manager2"
                },
                new Manager
                {
                    Id = managersIds[2],
                    Name = "Manager3"
                }
            );
            context.Products.AddRange(
                new Product
                {
                    Id = productsIds[0],
                    Name = "Product1"
                },
                new Product
                {
                    Id = productsIds[1],
                    Name = "Product2"
                },
                new Product
                {
                    Id = productsIds[2],
                    Name = "Product3"
                }
            );
            context.Orders.AddRange(
                new Order
                {
                    Id = ordersIds[0],
                    ClientId = clientsIds[0],
                    ManagerId = managersIds[0],
                    ProductId = productsIds[0],
                    Price = 10,
                    Date = new DateTime(2022, 1, 1)
                },
                new Order
                {
                    Id = ordersIds[1],
                    ClientId = clientsIds[1],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[1],
                    Price = 10,
                    Date = new DateTime(2022, 1, 2)
                },
                new Order
                {
                    Id = ordersIds[2],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[2],
                    ProductId = productsIds[2],
                    Price = 10,
                    Date = new DateTime(2022, 1, 3)
                },
                new Order
                {
                    Id = ordersIds[3],
                    ClientId = clientsIds[0],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[0],
                    Price = 10,
                    Date = new DateTime(2022, 1, 4)
                },
                new Order
                {
                    Id = ordersIds[4],
                    ClientId = clientsIds[0],
                    ManagerId = managersIds[0],
                    ProductId = productsIds[1],
                    Price = 10,
                    Date = new DateTime(2022, 1, 5)
                },
                new Order
                {
                    Id = ordersIds[5],
                    ClientId = clientsIds[1],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[0],
                    Price = 10,
                    Date = new DateTime(2022, 1, 6)
                },
                new Order
                {
                    Id = ordersIds[6],
                    ClientId = clientsIds[1],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[1],
                    Price = 10,
                    Date = new DateTime(2022, 1, 7)
                },
                new Order
                {
                    Id = ordersIds[7],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[0],
                    ProductId = productsIds[0],
                    Price = 10,
                    Date = new DateTime(2022, 1, 8)
                },
                new Order
                {
                    Id = ordersIds[8],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[0],
                    Price = 10,
                    Date = new DateTime(2021, 12, 1)
                },
                new Order
                {
                    Id = ordersIds[9],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[1],
                    Price = 10,
                    Date = new DateTime(2021, 12, 2)
                },
                new Order
                {
                    Id = ordersIds[10],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[2],
                    ProductId = productsIds[0],
                    Price = 10,
                    Date = new DateTime(2022, 1, 11)
                },
                new Order
                {
                    Id = ordersIds[11],
                    ClientId = clientsIds[1],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[1],
                    Price = 10,
                    Date = new DateTime(2022, 1, 2)
                },
                new Order
                {
                    Id = ordersIds[12],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[2],
                    ProductId = productsIds[2],
                    Price = 10,
                    Date = new DateTime(2022, 1, 3)
                },
                new Order
                {
                    Id = ordersIds[13],
                    ClientId = clientsIds[0],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[1],
                    Price = 20,
                    Date = new DateTime(2022, 1, 4)
                },
                new Order
                {
                    Id = ordersIds[14],
                    ClientId = clientsIds[0],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[1],
                    Price = 30,
                    Date = new DateTime(2022, 1, 5)
                },
                new Order
                {
                    Id = ordersIds[15],
                    ClientId = clientsIds[1],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[0],
                    Price = 40,
                    Date = new DateTime(2022, 1, 6)
                },
                new Order
                {
                    Id = ordersIds[16],
                    ClientId = clientsIds[1],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[1],
                    Price = 10,
                    Date = new DateTime(2022, 1, 7)
                },
                new Order
                {
                    Id = ordersIds[17],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[0],
                    ProductId = productsIds[2],
                    Price = 100,
                    Date = new DateTime(2022, 1, 8)
                },
                new Order
                {
                    Id = ordersIds[18],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[0],
                    Price = 10,
                    Date = new DateTime(2021, 12, 1)
                },
                new Order
                {
                    Id = ordersIds[19],
                    ClientId = clientsIds[2],
                    ManagerId = managersIds[1],
                    ProductId = productsIds[2],
                    Price = 10,
                    Date = new DateTime(2021, 12, 2)
                }
            );

            context.SaveChanges();
        }
    }
}