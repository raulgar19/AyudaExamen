using AyudaExamen.Data;
using AyudaExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace AyudaExamen.Repositories
{
    #region PROCEDURES
    //CREATE PROCEDURE SP_IMAGENES_ZAPATILLAS
    //(@ID INT, @POSICION INT)
    //AS
    //    SELECT id, comic_id, imagen_url FROM
    //        (SELECT CAST(
    //            ROW_NUMBER() OVER(ORDER BY IDIMAGEN) AS INT) 
    //   AS POSICION,
    //            IDIMAGEN, IDPRODUCTO, IMAGEN

    //        FROM IMAGENESZAPASPRACTICA

    //        WHERE IDPRODUCTO = @IDPRODUCTO) AS QUERY

    //    WHERE QUERY.POSICION = @POSICION
    //GO
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
            return await this.context.Imagenes.Where(z => z.Id == id).CountAsync();
        }

        //public async Task<Imagen> GetImagenByPosicionAsync(int id, int posicion)
        //{
        //    string sql = "SP_IMAGEN_COMIC @ID, @POSICION";
        //    SqlParameter pamIdZapatilla = new SqlParameter("@ID", id);
        //    SqlParameter pamPosicion = new SqlParameter("@POSICION", posicion);

        //    var consulta = await this.context.Imagenes.FromSqlRaw(sql, pamIdZapatilla, pamPosicion).ToListAsync();

        //    ImagenZapatilla imagen = consulta.FirstOrDefault();

        //    return imagen;
        //}
    }
}