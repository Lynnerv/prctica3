using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using prctica3.Models;
using prctica3.Services;

namespace prctica3.Controllers
{
    public class NoticiasController : Controller
    {
        private readonly ILogger<NoticiasController> _logger;
        private readonly PostService _postService;

        public NoticiasController(ILogger<NoticiasController> logger, PostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        // Muestra lista de publicaciones
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetEnrichedPostsAsync();
            return View(posts);
        }

        // Muestra detalles de un post
        public async Task<IActionResult> Detalle(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // Envía feedback 👍 👎 a la API local
        [HttpPost]
        public async Task<IActionResult> Reaccion(int postId, string sentimiento)
        {
            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync("http://localhost:5294/api/feedback",
                new { PostId = postId, Sentimiento = sentimiento, Fecha = DateTime.Now });

            if (!response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "⚠️ Ya reaccionaste a esta noticia.";
            }
            else
            {
                TempData["Mensaje"] = "✅ Reacción registrada correctamente.";
            }

            return RedirectToAction("Detalle", new { id = postId });
        }

        // Vista de error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
