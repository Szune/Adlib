using System;
using System.Collections.Generic;
using System.Linq;
using Adlib.Api;
using Adlib.Api.Models;
using Adlib.Api.Repositories;
using Adlib.Api.Services;
using Adlib.Database.Models;
using Moq;
using Xunit;

namespace Adlib.Tests
{
    public class AdServiceTests
    {
        protected readonly Mock<IRepository<Ad>> AdRepositoryMock;

        protected AdServiceTests()
        {
            AdRepositoryMock = new Mock<IRepository<Ad>>();
        }
    }

    public class Add : AdServiceTests
    {
        [Fact]
        public void ModelNotNull_ShouldAddToRepository()
        {
            // arrange
            var sut = new AdService(AdRepositoryMock.Object);
            var model = new AddAdModel
            {
                Subject = "subject",
                Body = "body",
                EmailAddress = "email",
                PriceSek = 500
            };
            // act
            sut.Add(model);
            // assert
            AdRepositoryMock.Verify(m => m.Add(It.IsAny<Ad>()));
        }

        [Fact]
        public void ModelNull_ShouldThrow()
        {
            var sut = new AdService(AdRepositoryMock.Object);
            void Act() => sut.Add(null);
            Assert.Throws<ArgumentNullException>(Act);
        }
    }

    public class Delete : AdServiceTests
    {
        [Fact]
        public void AboveZeroId_ShouldAddToRepository()
        {
            var sut = new AdService(AdRepositoryMock.Object);
            sut.Delete(1);
            AdRepositoryMock.Verify(m => m.Delete(It.IsAny<int>()));
        }

        [Fact]
        public void ZeroId_ShouldThrow()
        {
            var sut = new AdService(AdRepositoryMock.Object);
            void Act() => sut.Delete(0);
            Assert.Throws<ArgumentException>(Act);
        }

        [Fact]
        public void NegativeId_ShouldThrow()
        {
            var sut = new AdService(AdRepositoryMock.Object);
            void Act() => sut.Delete(-1);
            Assert.Throws<ArgumentException>(Act);
        }
    }

    public class Get : AdServiceTests
    {
        [Fact]
        public void RepositoryContainsAds_ShouldReturnAds()
        {
            // arrange
            AdRepositoryMock.Setup(m => m.Get()).Returns(new List<Ad>
            {
                new Ad {Body = "meh"}
            });
            var sut = new AdService(AdRepositoryMock.Object);
            // act
            var result = sut.Get(OrderAdBy.Time);
            // assert
            Assert.NotEmpty(result);
        }


        [Fact]
        public void RepositoryContainsAds_ShouldReturnSortedAds()
        {
            // arrange
            var secondOldest = new Ad
            {
                Subject = "secondOldest",
                Created = new DateTime(2001, 01, 25)
            };
            var oldest = new Ad
            {
                Subject = "oldest",
                Created = new DateTime(2000, 04, 12)
            };
            var newest = new Ad
            {
                Subject = "newest",
                Created = new DateTime(2019, 08, 11)
            };
            var secondNewest = new Ad
            {
                Subject = "secondNewest",
                Created = new DateTime(2018, 09, 29)
            };
            var ads = new List<Ad> { secondOldest, oldest, newest, secondNewest };
            AdRepositoryMock.Setup(m => m.Get()).Returns(ads);
            var sut = new AdService(AdRepositoryMock.Object);
            // act
            var result = sut.Get(OrderAdBy.Time).Select(a => a.Created);
            // assert
            var sorted = 
                new List<Ad> {newest, secondNewest, secondOldest, oldest}
                .Select(a => a.Created);
            Assert.True(sorted.SequenceEqual(result));
        }

        [Fact]
        public void RepositoryContainsNoAds_ShouldReturnEmptyResult()
        {
            var sut = new AdService(AdRepositoryMock.Object);
            var result = sut.Get(OrderAdBy.Time);
            Assert.Empty(result);
        }

        [Fact]
        public void ZeroId_ShouldThrow()
        {
            var sut = new AdService(AdRepositoryMock.Object);
            void Act() => sut.Delete(0);
            Assert.Throws<ArgumentException>(Act);
        }

        [Fact]
        public void NegativeId_ShouldThrow()
        {
            var sut = new AdService(AdRepositoryMock.Object);
            void Act() => sut.Delete(-1);
            Assert.Throws<ArgumentException>(Act);
        }
    }
}