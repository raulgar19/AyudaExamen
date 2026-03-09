using System.Security.Cryptography;
using System.Text;

namespace AyudaExamen.Helpers
{
    public class HelperCryptography
    {
        public static string Salt { get; set; }

        public static string EncriptarTextoBasico(string contenido)
        {
            byte[] entrada;
            byte[] salida;

            UnicodeEncoding encoding = new UnicodeEncoding();

            SHA1 managed = SHA1.Create();

            entrada = encoding.GetBytes(contenido);

            salida = managed.ComputeHash(entrada);

            string resultado = encoding.GetString(salida);

            return resultado;
        }

        public static string CifrarContenido(string contenido, bool comparar)
        {
            if (comparar == false)
            {
                Salt = GenerateSalt();
            }

            string contenidoSalt = contenido + Salt;

            SHA512 managed = SHA512.Create();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] salida;

            salida = encoding.GetBytes(contenidoSalt);

            for (int i = 1; i <= 21; i++)
            {
                salida = managed.ComputeHash(salida);
            }

            managed.Clear();

            string resultado = encoding.GetString(salida);

            return resultado;
        }

        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";

            for (int i = 1; i < 30; i++)
            {
                int num = random.Next(1, 255);
                char letra = Convert.ToChar(num);
                salt += letra;
            }

            return salt;
        }
    }
}