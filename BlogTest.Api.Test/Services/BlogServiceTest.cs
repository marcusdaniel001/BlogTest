using Moq;
using Domain.Entities;
using Domain.Services;
using Domain.DTOs;
using Domain.Interfaces.Repositories;

namespace BlogTest.Api.Test.Services
{
    public class BlogServiceTests
    {
        private readonly Mock<IBlogRepository> _mockRepository;
        private readonly BlogService _blogService;

        public BlogServiceTests()
        {
            _mockRepository = new Mock<IBlogRepository>();
            _blogService = new BlogService(_mockRepository.Object);
        }

        [Fact]
        public void GetBlogPostById_ReturnsPost()
        {
            // Arrange
            var postId = 1;
            var expectedPost = new Post { PostId = postId, Title = "Test Post" };
            _mockRepository.Setup(repo => repo.GetBlogPostById(postId)).Returns(expectedPost);

            // Act
            var result = _blogService.GetBlogPostById(postId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(postId, result.PostId);
            Assert.Equal("Test Post", result.Title);
        }

        [Fact]
        public void GetAllBlogPosts_ReturnsAllPosts()
        {
            // Arrange
            var posts = new List<PostsDto>
            {
                new PostsDto { PostId = 1, Title = "Post 1", CommentsCounter = 2 },
                new PostsDto { PostId = 2, Title = "Post 2", CommentsCounter = 3 }
            };
            _mockRepository.Setup(repo => repo.GetAllBlogPosts()).Returns(posts);

            // Act
            var result = _blogService.GetAllBlogPosts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Post 1", result.First().Title);
        }

        [Fact]
        public void CreatePost_AddsPost()
        {
            // Arrange
            var newPost = new Post { PostId = 3, Title = "Post 3", Content = "Content 3" };
            _mockRepository.Setup(repo => repo.CreatePost(newPost)).Returns(true);

            // Act
            var result = _blogService.CreatePost(newPost);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CreateComment_AddsComment()
        {
            // Arrange
            var postId = 1;
            var newComment = new Comment { CommentId = 4, Text = "Comment 4" };
            _mockRepository.Setup(repo => repo.CreateComment(postId, newComment)).Returns(true);

            // Act
            var result = _blogService.CreateComment(postId, newComment);

            // Assert
            Assert.True(result);
        }
    }
}
