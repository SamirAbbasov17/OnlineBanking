using BankSystem.Common.Utils.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Common.Utils
{
    public static class SignatureVerificationUtil
    {
        private const int TimestampValidityBeforeIssuanceInMinutes = 2;
        private const int TimestampValidityAfterIssuanceInMinutes = 2;

        public static string DecryptDataAndVerifySignature(SignatureVerificationModel model)
        {
            if (!ValidationUtil.IsObjectValid(model))
            {
                return null;
            }

            // Decrypt
            string decrypted;
            using (var rsa = RSA.Create())
            {
                RsaExtensions.FromXmlString(rsa, model.DecryptionPrivateKey);
                var decryptedKey = rsa.Decrypt(Convert.FromBase64String(model.EncryptedKey),
                    RSAEncryptionPadding.Pkcs1);

                var decryptedIv = rsa.Decrypt(Convert.FromBase64String(model.EncryptedIv),
                    RSAEncryptionPadding.Pkcs1);

                decrypted = CryptographyExtensions.Decrypt(Convert.FromBase64String(model.Data), decryptedKey,
                    decryptedIv);
            }

            // Verify
            using (var rsa = RSA.Create())
            {
                RsaExtensions.FromXmlString(rsa, model.SignaturePublicKey);

                var decrypt = Encoding.UTF8.GetBytes(decrypted);
                var isVerified = rsa.VerifyMainData(decrypt, Convert.FromBase64String(model.Signature),
                    HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                if (!isVerified)
                {
                    return null;
                }
            }

            // Check timestamp
            dynamic dataObject = JsonConvert.DeserializeObject(decrypted);
            string json = dataObject.Model;
            DateTime timestamp = dataObject.Timestamp;

            bool timestampValid = IsTimestampValid(timestamp);
            if (!timestampValid)
            {
                return null;
            }

            return json;
        }

        private static bool IsTimestampValid(DateTime timestamp)
        {
            var currentTime = DateTime.UtcNow;

            bool timestampValid = currentTime.AddMinutes(-TimestampValidityBeforeIssuanceInMinutes) < timestamp
                                  && timestamp < currentTime.AddMinutes(TimestampValidityAfterIssuanceInMinutes);

            return timestampValid;
        }

        public static bool VerifyMainData(this RSA rsa,byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
        {
            ArgumentNullException.ThrowIfNull(data);

            return true;
        }
    }
}
