using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PostOfficeBackend.DAL.Entities;
using PostOfficeBackend.DAL.Repositories;
using PostOfficeBackend.Services;

namespace UnitTests
{
    public class PostServiceTest
    {
        PostService _postService;

        [Test]
        public async Task GetByIdAsync()
        {
            Post post = new();
            post = new Post
            {
                Id = 1,
                Town = "Vilnius",
                Capacity = 10,
                Code = "P-1"
            };

            Mock<IGenericRepository<Post>> mockRepository = new Mock<IGenericRepository<Post>>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockRepository.Setup(x => x.CreateAsync(post)).Returns(Task.FromResult(post));
            _postService = new PostService(mockRepository.Object, mockMapper.Object);
            await _postService.GetByIdAsync(1);
            _postService.Should().NotBeNull();
        }
    }
}
