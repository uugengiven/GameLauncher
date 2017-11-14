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

        public string Encrypt(string value, string key)
        {
            var secretKey = System.Convert.FromBase64String(key);
            return Jose.JWT.Encode(value, secretKey, JwsAlgorithm.HS256);
        }

        public string Encrypt(string value)
        {
            return Encrypt(value, secretKeybit);
        }

        public string Decrypt(string value, string key)
        {
            var secretKey = System.Convert.FromBase64String(key);
            return Jose.JWT.Decode(value, secretKey, JwsAlgorithm.HS256);
        }

        public string Decrypt(string value)
        {
            return Decrypt(value, secretKeybit);
        }

        public string getSalt(int max_length)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[max_length];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public string GetUniqueKey(int maxSize)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data); // unsure why this is here, maybe to cause an extra call to the rng? (came from https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c )
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
