using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSAGenerator
{
    public static class Generator
    {
        public static void Main()
        {
            var rsa = new RSACryptoServiceProvider(4096);
            var publicKey = rsa.ToXmlString(false);
            var privateKey = rsa.ToXmlString(true);
            Console.WriteLine($"Public key: {publicKey}");
            Console.WriteLine("=====================================================================");
            Console.WriteLine($"Private key: {privateKey}");
        }
    }
}
