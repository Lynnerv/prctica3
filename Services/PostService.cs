using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using prctica3.Models;

namespace prctica3.Services
{
    public class PostService
    {
        private readonly HttpClient _client;

        public PostService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<PostEnriquecido>> GetEnrichedPostsAsync()
        {
            var posts = await _client.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");
            var users = await _client.GetFromJsonAsync<List<User>>("https://jsonplaceholder.typicode.com/users");
            var comments = await _client.GetFromJsonAsync<List<Comment>>("https://jsonplaceholder.typicode.com/comments");

            return posts.Select(post => new PostEnriquecido
            {
                Post = post,
                Autor = users.FirstOrDefault(u => u.Id == post.UserId),
                Comentarios = comments.Where(c => c.PostId == post.Id).ToList()
            }).ToList();
        }

        public async Task<PostEnriquecido?> GetPostByIdAsync(int id)
        {
            var enrichedPosts = await GetEnrichedPostsAsync();
            return enrichedPosts.FirstOrDefault(p => p.Post.Id == id);
        }
    }
}
