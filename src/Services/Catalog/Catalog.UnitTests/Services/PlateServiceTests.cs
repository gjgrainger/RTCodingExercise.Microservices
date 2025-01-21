using Catalog.API.Data;
using Catalog.API.Repositories;
using Catalog.Domain;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using System;
using Catalog.Business.Services;
using AutoMapper;
using Catalog.API.DTOs;
using Catalog.API.Profiles;

namespace Catalog.UnitTests.Services
{
    public class PlateServiceTests
    {
        private ApplicationDbContext _dbContext;
        private PlateRepository _repository;
        private PlateService _plateService;
        private Mapper _mapper;
        private PlateDto _newPlate;

        [SetUp]
        public void SetUp()
        {
            _dbContext = ContextHelper.CreateContext();
            ContextHelper.SeedContext(_dbContext);
            _repository = new PlateRepository(_dbContext);
            _mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMap<Plate, PlateDto>();
                config.AddProfile<PlateProfile>();
            }));
            _plateService = new PlateService(_repository, _mapper);
            _newPlate = new PlateDto
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
        public async Task Create_PlateAlreadyExists_ThrowsException()
        {
            _newPlate.Id = _plateService.GetAll().First().Id;

            await _plateService.Invoking(async x => await x.Create(_newPlate))
                .Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task Create_PlateDoesntExists_CreatesPlate()
        {
            await _plateService.Create(_newPlate);
            await _dbContext.SaveChangesAsync();

            var exists = await _repository.Exists(_newPlate.Id);
            exists.Should().BeTrue();
        }

        [Test]
        public async Task GetPagedAndFilteredResults_InputPageSize_ReturnsResultSizeOfPageIndex()
        {
            var results = await _plateService.GetPagedAndFilteredResults(1, 1, string.Empty);
            results.Plates.Should().HaveCount(1);
            results.TotalPlates.Should().Be(ContextHelper._plates.Count);
        }

        [Test]
        public async Task GetPagedAndFilteredResults_InputPageIndex_ReturnsResultWithCorrectIndex()
        {
            var results = await _plateService.GetPagedAndFilteredResults(2, 1, string.Empty);
            results.Plates.First().Registration.Should().Be(ContextHelper._plates.Skip(1).First().Registration);
            results.TotalPlates.Should().Be(ContextHelper._plates.Count);
        }

        [Test]
        public async Task GetPagedAndFilteredResults_MatchingFilter_ReturnsFilteredResult()
        {
            var results = await _plateService.GetPagedAndFilteredResults(1, 1, ContextHelper._plates.First().Registration);
            results.Plates.First().Registration.Should().Be(ContextHelper._plates.First().Registration);
            results.TotalPlates.Should().Be(ContextHelper._plates.Count);
        }

        [Test]
        public async Task GetPagedAndFilteredResults_NoMatchingFilter_ReturnsZeroPlates()
        {
            var results = await _plateService.GetPagedAndFilteredResults(2, 1, "Nonsense search");
            results.Plates.Count.Should().Be(0);
            results.TotalPlates.Should().Be(ContextHelper._plates.Count);
        }
    }
}
