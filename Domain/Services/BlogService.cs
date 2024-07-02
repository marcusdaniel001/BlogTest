using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class BlogService(IBlogRepository blogRepository) : IBlogService
    {
        private readonly IBlogRepository _blogRepository = blogRepository;

        public Post GetBlogPostById(int id)
        {
            try
            {
                return _blogRepository.GetBlogPostById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PostsDto> GetAllBlogPosts()
        {
            try
            {
                return _blogRepository.GetAllBlogPosts();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CreatePost(Post post)
        {
            try
            {
                return _blogRepository.CreatePost(post);
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
                return _blogRepository.CreateComment(id, comment);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
