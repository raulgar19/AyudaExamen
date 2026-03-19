namespace AyudaExamen.Models
{
    public class ComicImagesModel
    {
        public int Registros { get; set; }

        public Comic Comic { get; set; }

        public List<Imagen> Imagenes { get; set; }
    }
}