//  Erasmo Cardoso


using Microsoft.EntityFrameworkCore;
using AcessoAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AcessoAPI.API.DadosApiExt;

namespace AcessoAPI.Repositories
{
    public class PostRepository
    {
        private readonly ControleSistemaContext _context;

        public PostRepository(ControleSistemaContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();


        }
    }
}
