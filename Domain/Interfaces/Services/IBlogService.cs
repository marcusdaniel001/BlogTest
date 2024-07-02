using Domain.DTOs;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IBlogService
    {
        IEnumerable<PostsDto> GetAllBlogPosts();
        Post GetBlogPostById(int id);
        bool CreatePost(Post blogPost);
        bool CreateComment(int id, Comment comment);
    }
}
