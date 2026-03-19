using AyudaExamen.Data;
using AyudaExamen.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AyudaExamen.Repositories
{
    #region PROCEDURES
    //CREATE PROCEDURE SP_IMAGEN_COMIC
    //(@id INT, @posicion INT)
    //AS
    //    SELECT id, comic_id, imagen_url FROM
    //        (SELECT CAST(
    //            ROW_NUMBER() OVER(ORDER BY id) AS INT) 
    //   AS posicion,
    //            id, comic_id, imagen_url

    //        FROM imagenes

    //        WHERE comic_id = @id) AS QUERY

    //    WHERE QUERY.posicion = @posicion
    //GO

    //CREATE PROCEDURE SP_IMAGENES_COMIC
    //@id       INT,
    //@posicion INT
    //AS
    //    SELECT
    //        QUERY.id,
    //        QUERY.comic_id,
    //        QUERY.imagen_url
    //    FROM(
    //        SELECT
    //            ROW_NUMBER() OVER (ORDER BY id) AS posicion,
    //            id,
    //            comic_id,
    //            imagen_url
    //        FROM imagenes
    //        WHERE comic_id = @id
    //    ) AS QUERY
    //    WHERE QUERY.posicion >= @posicion
    //      AND QUERY.posicion<(@posicion + 2);
    //    GO
    #endregion

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
            return await this.context.Imagenes
                .Where(z => z.ComicId == id)
                .CountAsync();
        }

        public async Task<Imagen> GetImagenByPosicionAsync(int id, int posicion)
        {
            string sql = "SP_IMAGEN_COMIC @posicion, @id";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdComic = new SqlParameter("@id", id);

            var consulta = await this.context.Imagenes.FromSqlRaw(sql, pamIdComic, pamPosicion).ToListAsync();

            Imagen imagen = consulta.FirstOrDefault();

            return imagen;
        }

        public async Task<List<Imagen>> GetImagenesAsync(int id, int posicion)
        {
            string sql = "SP_IMAGENES_COMIC @id, @posicion";
            SqlParameter pamIdComic = new SqlParameter("@id", id);
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);

            var consulta = this.context.Imagenes.FromSqlRaw(sql, pamIdComic, pamPosicion);

            return await consulta.ToListAsync();
        }
    }
}