using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogTest.Api.Test.Repositories
{
    public class BlogRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly AppDbContext _context;
        private readonly BlogRepository _repository;

        public BlogRepositoryTests()
        {
            // Configure in-memory database for testing
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogTestDb")
                .Options;
            _context = new AppDbContext(_options);
            _repository = new BlogRepository(_context);

            // Seed the database
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var posts = new List<Post>
            {
                new Post { PostId = 1, Title = "Post 1", Content = "Content 1" },
                new Post { PostId = 2, Title = "Post 2", Content = "Content 2" }
            };

            var comments = new List<Comment>
            {
                new Comment { CommentId = 1, PostId = 1, Text = "Comment 1" },
                new Comment { CommentId = 2, PostId = 1, Text = "Comment 2" },
                new Comment { CommentId = 3, PostId = 2, Text = "Comment 3" }
            };

            _context.Posts.AddRange(posts);
            _context.Comments.AddRange(comments);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllBlogPosts_ReturnsAllPosts()
        {
            // Act
            var result = _repository.GetAllBlogPosts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(2, result.First().CommentsCounter);
        }

        [Fact]
        public void GetBlogPostById_ReturnsPost()
        {
            // Act
            var result = _repository.GetBlogPostById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.PostId);
            Assert.Equal(2, result.Comments.Count);
        }

        [Fact]
        public void CreatePost_AddsPost()
        {
            // Arrange
            var newPost = new Post { PostId = 3, Title = "Post 3", Content = "Content 3" };

            // Act
            var result = _repository.CreatePost(newPost);

            // Assert
            Assert.True(result);
            Assert.Equal(3, _context.Posts.Count());
        }

        [Fact]
        public void CreateComment_AddsComment()
        {
            // Arrange
            var newComment = new Comment { CommentId = 4, Text = "Comment 4" };

            // Act
            var result = _repository.CreateComment(1, newComment);

            // Assert
            Assert.True(result);
            Assert.Equal(3, _context.Comments.Count(c => c.PostId == 1));
        }
    }
}
