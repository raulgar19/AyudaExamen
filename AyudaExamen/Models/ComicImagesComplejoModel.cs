namespace AyudaExamen.Models
{
    public class ComicImagesComplejoModel
    {
        public int Registros { get; set; }

        public Comic Comic { get; set; }

        public List<Imagen> Imagenes { get; set; }
    }
}
