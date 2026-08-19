using System.Security.Cryptography;
using System.Text;

namespace SmartQueue.Helpers
{
    // Genera un hash SHA-256 de la contrasena para no guardarla en texto plano
    public static class Seguridad
    {
        public static string Hashear(string texto)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            byte[] hash = SHA256.HashData(bytes);

            StringBuilder resultado = new StringBuilder();
            foreach (byte b in hash)
            {
                resultado.Append(b.ToString("x2"));
            }
            return resultado.ToString();
        }
    }
}
