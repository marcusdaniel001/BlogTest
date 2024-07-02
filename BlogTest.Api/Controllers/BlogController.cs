using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController(IBlogService blogService, ILogger<BlogController> logger) : ControllerBase
    {
        private readonly IBlogService _blogService = blogService;
        private readonly ILogger<BlogController> _logger = logger;

        /// <summary>
        /// Method responsible for searching all posts contained in the blog
        /// </summary>
        /// <returns>Returns list of all Posts</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAllBlogPosts()
        {
            try
            {
                return Ok(_blogService.GetAllBlogPosts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when fetching all blog posts {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Method responsible for searching the post by a specific id
        /// </summary>
        /// <param name="id">Id of Post</param>
        /// <returns>Returns the Post entity with its list of comments</returns>
        [HttpGet("{id}")]
        public ActionResult<Post> GetBlogPostById(int id)
        {
            try
            {
                var blogPost = _blogService.GetBlogPostById(id);

                if (blogPost == null)
                {
                    return NotFound("No posts found for this Id");
                }

                return blogPost;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when fetching post by Id {ex.Message}");
                throw;
            }

        }

        /// <summary>
        /// Method responsible for creating a new post
        /// </summary>
        /// <param name="post">Post Object</param>
        /// <returns>Returns message whether the post was created successfully or not</returns>
        [HttpPost]
        public ActionResult<Post> CreatePost(Post post)
        {
            try
            {
                var created = _blogService.CreatePost(post);

                if (created)
                    return Ok($"Post successfully created with title {post.Title}");
                else
                    return BadRequest("Error when creating a new post, try again later");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when creating a new post {ex.Message}");
                throw;
            }

        }

        /// <summary>
        /// Method responsible for creating a new comment on an existing post
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <param name="comment">Comment Object</param>
        /// <returns>Returns message whether the comment was created successfully or not</returns>
        [HttpPost("{id}/comments")]
        public ActionResult<Comment> CreateComment(int id, Comment comment)
        {
            try
            {
                var post = _blogService.GetBlogPostById(id);

                if (post == null)
                {
                    return NotFound("No posts found with this id");
                }

                var createdComment = _blogService.CreateComment(id, comment);

                if (createdComment)
                    return Ok($"Comment successfully created for the post {post.Title}");
                else
                    return BadRequest("Error when creating a new comment, try again later");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when creating a new comment {ex.Message}");
                throw;
            }

        }
    }
}
