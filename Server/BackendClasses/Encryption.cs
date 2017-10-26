using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LauncherServerClasses
{
    public class Encryption
    {
        // Do not write your own encryption. Use libraries like JWT
        // this needs to eventually take in the secretKeybit and salt for encryption/decryption

        public string Encrypt(string value)
        {
            var secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
            return Jose.JWT.Encode(value, secretKey, JwsAlgorithm.HS256);
        }

        public string Decrypt(string value)
        {
            var secretKeybit = "pDzCAKG9KSaCWY2kLaqf0UWJ89i/gy/6IGvndSWe4eo=";
            var secretKey = System.Convert.FromBase64String(secretKeybit);
            return Jose.JWT.Decode(value, secretKey, JwsAlgorithm.HS256);
        }

        public string getSalt(int max_length)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[max_length];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
