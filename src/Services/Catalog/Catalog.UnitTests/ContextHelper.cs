using Catalog.API.Data;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Catalog.UnitTests
{
    public class ContextHelper
    {
        internal static List<Plate> _plates = new List<Plate>()
        {
            new Plate
                {
                    Id = Guid.NewGuid(),
                    Letters = "GEORGE",
                    Numbers = 1,
                    PurchasePrice = 1000,
                    SalePrice = 2000,
                    Registration = "GEORGE1"
                },
                new Plate
                {
                    Id = Guid.NewGuid(),
                    Letters = "GEORGE",
                    Numbers = 2,
                    PurchasePrice = 3000,
                    SalePrice = 4000,
                    Registration = "GEORGE2"
                },
                new Plate
                {
                    Id = Guid.NewGuid(),
                    Letters = "GEORGE",
                    Numbers = 3,
                    PurchasePrice = 5000,
                    SalePrice = 6000,
                    Registration = "GEORGE3"
                }
        };

        internal static ApplicationDbContext CreateContext()
        {
            var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .LogTo(Console.WriteLine)
                .Options);

            context.Database.EnsureCreated();
            return context;
        }

        internal static void SeedContext(ApplicationDbContext context)
        {
            context.Plates.AddRange(_plates);

            context.SaveChanges();
        }
    }
}
