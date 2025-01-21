using Catalog.API.Data;
using Catalog.API.Repositories;
using Catalog.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.UnitTests.Repositories
{
    [TestFixture]
    public class RepositoryTests
    {
        private ApplicationDbContext _dbContext;
        private Repository<Plate> _repository;
        private Plate _newPlate;

        [SetUp]
        public void SetUp()
        {
            _dbContext = ContextHelper.CreateContext();
            ContextHelper.SeedContext(_dbContext);
            _repository = new Repository<Plate>(_dbContext);
            _newPlate = new Plate
            {
                Letters = "GEORGE",
                Numbers = 4,
                Registration = "GEORGE4",
                PurchasePrice = 7000,
                SalePrice = 8000
            };

        }

        [Test]
        public async Task Add_IdAlreadyExists_ThrowsException()
        {
            _newPlate.Id = _repository.GetAll().First().Id;

            await _repository.Invoking(async x => await x.Add(_newPlate))
                .Should().ThrowAsync<Exception>()
                .WithMessage($"{typeof(Plate).Name} with Id {_newPlate.Id} already exists");
        }

        [Test]
        public async Task Add_IdDoesntExist_PlateAdded()
        {
            _newPlate.Id = Guid.NewGuid();

            var plate = await _repository.Add(_newPlate);
            await _dbContext.SaveChangesAsync();

            var exists = await _repository.Exists(plate.Id);
            exists.Should().BeTrue();
        }

        [Test]
        public async Task Exists_IdExists_ReturnsTrue()
        {
            var existantPlate = _repository.GetAll().First();

            var exists = await _repository.Exists(existantPlate.Id);

            exists.Should().BeTrue();
        }

        [Test]
        public async Task Exists_IdDoesntExist_ReturnsFalse()
        {
            var exists = await _repository.Exists(Guid.NewGuid());

            exists.Should().BeFalse();
        }

        [Test]
        public async Task GetAll_ReturnsCorrectNumberOfPlates()
        {
            var count = await _repository.GetAll().CountAsync();

            count.Should().Be(ContextHelper._plates.Count);
        }

        [Test]
        public async Task GetAll_UponAddingPlate_ReturnsCorrectNumberOfPlates()
        {
            _newPlate.Id = Guid.NewGuid();

            var plate = await _repository.Add(_newPlate);
            await _dbContext.SaveChangesAsync();

            var count = await _repository.GetAll().CountAsync();

            count.Should().Be(ContextHelper._plates.Count + 1);
        }
    }
}
