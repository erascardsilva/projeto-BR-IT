//Erasmo Cardoso
using Microsoft.AspNetCore.Mvc;
using AcessoAPI.Repositories;
using AcessoAPI.API; 
using System.Threading.Tasks;

namespace AcessoAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostRepository _postRepository;
        private readonly DadosApiExt _dadosApiExt; 

        public PostsController(PostRepository postRepository, DadosApiExt dadosApiExt) 
        {
            _postRepository = postRepository;
            _dadosApiExt = dadosApiExt; 
        }

        [HttpGet]
        public async Task<ActionResult<List<DadosApiExt.Post>>> GetPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();

            if (posts == null || posts.Count == 0)
            {
                return NotFound("Nenhum post encontrado.");
            }

            return Ok(posts);
        }

        [HttpPost("importar")] 
        public async Task<IActionResult> ImportPosts()
        {
            await _dadosApiExt.InsertPostsIntoDatabase(); 

            return Ok("Posts importados com sucesso.");
        }
    }
}
