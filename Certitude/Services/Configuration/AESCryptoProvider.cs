using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Originally from http://stackoverflow.com/questions/165808/simple-2-way-encryption-for-c-sharp
// Updated to according to my needs
// - Made static
// - Updated keys
// - Changed to Unicode and Base64 from UTF and ASCII
// - Removed unused code

namespace Certitude.Services.Configuration
{
    public static class AESCryptoProvider
    {
        // Change these keys to keep each implementation unique - I use http://www.random.org
        // read these from config, that way we can change them in production easily
        private static readonly byte[] Key = Convert.FromBase64String(ServiceFactory.ConfigurationService.ReadValue("encryption", "key"));
        private static readonly byte[] Vector = Convert.FromBase64String(ServiceFactory.ConfigurationService.ReadValue("encryption", "vector"));

        private static readonly RijndaelManaged RijndaelManaged = new RijndaelManaged();
        private static readonly ICryptoTransform EncryptorTransform = RijndaelManaged.CreateEncryptor(Key, Vector);
        private static readonly ICryptoTransform DecryptorTransform = RijndaelManaged.CreateDecryptor(Key, Vector);
        private static readonly UnicodeEncoding Encoder = new UnicodeEncoding();

        /// Encrypt some text and return a string
        public static string Encrypt(string textValue)
        {
            return Convert.ToBase64String(InnerEncrypt(textValue));
        }

        /// Encrypt some text and return an encrypted byte array.
        private static byte[] InnerEncrypt(string textValue)
        {
            //Translates our text value into a byte array.
            Byte[] bytes = Encoder.GetBytes(textValue);

            //Used to stream the data in and out of the CryptoStream.
            MemoryStream memoryStream = new MemoryStream();

            /*
         * We will have to write the unencrypted bytes to the stream,
         * then read the encrypted result back from the stream.
         */
            #region Write the decrypted value to the encryption stream
            CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            #endregion

            #region Read encrypted value back out of the stream
            memoryStream.Position = 0;
            byte[] encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);
            #endregion

            //Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        /// The other side: Decryption methods
        public static string Decrypt(string encryptedString)
        {
            return InnerDecrypt(Convert.FromBase64String(encryptedString));
        }

        /// Decryption when working with byte arrays.    
        private static string InnerDecrypt(byte[] encryptedValue)
        {
            #region Write the encrypted value to the decryption stream
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
            decryptStream.Write(encryptedValue, 0, encryptedValue.Length);
            decryptStream.FlushFinalBlock();
            #endregion

            #region Read the decrypted value from the stream.
            encryptedStream.Position = 0;
            Byte[] decryptedBytes = new Byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();
            #endregion
            return Encoder.GetString(decryptedBytes);
        }
    }
}