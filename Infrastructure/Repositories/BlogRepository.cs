using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BlogRepository(AppDbContext context) : IBlogRepository
    {
        private readonly AppDbContext _context = context;

        public IEnumerable<PostsDto> GetAllBlogPosts()
        {
            try
            {
                var posts = _context.Posts
                .Include(b => b.Comments)
                .Select(b => new PostsDto
                {
                    PostId = b.PostId,
                    Title = b.Title,
                    CommentsCounter = b.Comments.Count
                });

                return posts;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public Post GetBlogPostById(int id)
        {
            try
            {
                var blogPost = _context.Posts
                .Include(b => b.Comments)
                .FirstOrDefault(b => b.PostId == id);

                return blogPost;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool CreatePost(Post blogPost)
        {
            try
            {
                _context.Posts.Add(blogPost);
                var saved = _context.SaveChanges();

                return saved > 0;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool CreateComment(int id, Comment comment)
        {
            try
            {
                comment.PostId = id;
                _context.Comments.Add(comment);
                var saved = _context.SaveChanges();

                return saved > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
