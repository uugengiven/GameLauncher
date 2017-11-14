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
        private string secretKeybit;

        public Encryption(string key)
        {
            secretKeybit = key;
        }

        public string Encrypt(string value)
        {
            var secretKey = System.Convert.FromBase64String(secretKeybit);
            return Jose.JWT.Encode(value, secretKey, JwsAlgorithm.HS256);
        }

        public string Decrypt(string value)
        {
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
