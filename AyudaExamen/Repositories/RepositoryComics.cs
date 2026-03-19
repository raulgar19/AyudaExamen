using AyudaExamen.Data;
using AyudaExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace AyudaExamen.Repositories
{
    public class RepositoryComics
    {
        private ComicsContext context;
        public RepositoryComics(ComicsContext context)
        {
            this.context = context;
        }

        public async Task<List<Comic>> GetComicsAsync()
        {
            return await this.context.Comics.ToListAsync();
        }

        public async Task<Comic> FindComicAsync(int id)
        {
            return await this.context.Comics.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetRegistrosAsync(int id)
        {
            return await this.context.Imagenes.Where(z => z.Id == id).CountAsync();
        }

        public async Task<Imagen> GetImagenByPosicionAsync(int id, int value)
        {
            throw new NotImplementedException();
        }
    }
}