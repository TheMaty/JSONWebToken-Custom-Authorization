using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoRandomDataGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[512];
            cryptoRandomDataGenerator.GetBytes(buffer);
            string uniq = Convert.ToBase64String(buffer);
            Console.WriteLine(uniq);
        }
    }
}
