using Catalog.API.Data;
using Catalog.API.Repositories;
using Catalog.Domain;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.UnitTests.Repositories
{
    public class PlateRepositoryTests
    {
        private ApplicationDbContext _dbContext;
        private PlateRepository _repository;
        private Plate _newPlate;

        [SetUp]
        public void SetUp()
        {
            _dbContext = ContextHelper.CreateContext();
            ContextHelper.SeedContext(_dbContext);
            _repository = new PlateRepository(_dbContext);
            _newPlate = new Plate
            {
                Id = Guid.NewGuid(),
                Letters = "GEORGE",
                Numbers = 4,
                Registration = "GEORGE4",
                PurchasePrice = 7000,
                SalePrice = 8000
            };
        }

        [Test]
        public async Task Add_RegistrationAlreadyExists_ThrowsException()
        {
            _newPlate.Registration = _repository.GetAll().First().Registration;

            await _repository.Invoking(async x => await x.Add(_newPlate))
                .Should().ThrowAsync<Exception>()
                .WithMessage($"{typeof(Plate).Name} with Registration {_newPlate.Registration} already exists");
        }

        [Test]
        public async Task Add_RegistrationDoesntExist_PlateAdded()
        {
            var plate = await _repository.Add(_newPlate);
            await _dbContext.SaveChangesAsync();

            var exists = await _repository.Exists(plate.Id);
            exists.Should().BeTrue();
        }

        [Test]
        public async Task Exists_RegistrationExists_ReturnsTrue()
        {
            var existantPlate = _repository.GetAll().First();

            var exists = await _repository.Exists(existantPlate.Registration);

            exists.Should().BeTrue();
        }

        [Test]
        public async Task Exists_IdDoesntExist_ReturnsFalse()
        {
            var exists = await _repository.Exists(_newPlate.Registration);

            exists.Should().BeFalse();
        }
    }
}
