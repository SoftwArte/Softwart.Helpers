/************************************************
 *	Crytographic helper classes			
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/

namespace Softwarte.Helpers
{

  using System;
  using System.IO;
  using System.Security.Cryptography;
  using System.Text;
  using System.Linq;

  public sealed partial class HasherHelper
  {
    public enum HashAlgorithm
    {
      Sha256, Sha384, Sha512
    }
    /// <summary>
    /// Create and return a hash of string value passed, 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string CreateHash(string value, HashAlgorithm algorithm = HashAlgorithm.Sha512)
    {
      switch (algorithm)
      {
        case HashAlgorithm.Sha256:
          throw new NotImplementedException();
        case HashAlgorithm.Sha384:
          throw new NotImplementedException();
        case HashAlgorithm.Sha512:
          return Convert.ToBase64String(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
        default:
          return Convert.ToBase64String(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
      }
    }
    /// <summary>
    /// Create a return a hash of byte array.
    /// </summary>
    /// <param name="byteArray"></param>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public static string CreateHash(byte[] byteArray, HashAlgorithm algorithm = HashAlgorithm.Sha512)
    {
      switch (algorithm)
      {
        case HashAlgorithm.Sha256:
          throw new NotImplementedException();
        case HashAlgorithm.Sha384:
          throw new NotImplementedException();
        case HashAlgorithm.Sha512:
          return Convert.ToBase64String(SHA512.Create().ComputeHash(byteArray));
        default:
          return Convert.ToBase64String(SHA512.Create().ComputeHash(byteArray));
      }
    }
  }
  /// <summary>
  /// Helpers class to cypher strings.
  /// </summary>
  /// <requirements>Ninguno</requirements>
  public sealed partial class CryptoHelper
  {
    /// <summary>
    /// Encode a string with DES method.
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    [Obsolete]
    public static string EncryptDes(string texto)
    {
      byte[] vector = ASCIIEncoding.ASCII.GetBytes("elpluto1");
      byte[] key = ASCIIEncoding.ASCII.GetBytes("elpluto1");
      byte[] source = ASCIIEncoding.ASCII.GetBytes(texto);

      using (var desCsp = new DESCryptoServiceProvider())
      {
        using (var memStream = new MemoryStream())
        {
          using (var cryptoStr = new CryptoStream(memStream, desCsp.CreateEncryptor(key, vector), CryptoStreamMode.Write))
          {
            var writer = new StreamWriter(cryptoStr);
            writer.Write(source);
            writer.Flush();
            //Convert to a Base64 string.
            var result = Convert.ToBase64String(memStream.ToArray());
            return result;
          }
        }
      }
    }
    /// <summary>
    /// Decode a string encoded with DES.
    /// </summary>
    /// <param name="cryptedText"></param>
    /// <returns></returns>
    [Obsolete]
    public static string DecryptDes(string cryptedText)
    {
      byte[] vector = ASCIIEncoding.ASCII.GetBytes("elpluto1");
      byte[] key = ASCIIEncoding.ASCII.GetBytes("elpluto1");
      //Convert base64 encrypt string to byte[] array.
      byte[] source = Convert.FromBase64String(cryptedText);

      using (var desCsp = new DESCryptoServiceProvider())
      {
        using (var memStream = new MemoryStream())
        {
          using (var cryptoStr = new CryptoStream(memStream, desCsp.CreateDecryptor(key, vector), CryptoStreamMode.Write))
          {
            var writer = new StreamWriter(cryptoStr);
            writer.Write(source);
            writer.Flush();
            //Get the unencrypt string.
            var result = Convert.ToString(memStream.ToArray());
            return result;
          }
        }
      }
    }
    /// <summary>
    /// Decode a entire file with DecrypDes method.
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    public static string DecrypFileContent(string filepath)
    {
      using (var oFile = File.Open(filepath, FileMode.Open, FileAccess.Read))
      {
        using (var oRdr = new StreamReader(oFile))
        {
          string fileContent = oRdr.ReadToEnd();
          string Resultado = CryptoHelper.DecryptDes(fileContent);
          return Resultado;
        }

      }
    }
    /// <summary>
    /// Encode entire file with EncryptDes method.
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    public static string EncryptFileContent(string filepath)
    {
      using (var oFile = File.Open(filepath, FileMode.Open, FileAccess.Read))
      {
        using (var oRdr = new StreamReader(oFile))
        {
          string fileContent = oRdr.ReadToEnd();
          string Resultado = CryptoHelper.EncryptDes(fileContent);
          return Resultado;
        }
      }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="textToEncrypt"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <remarks>Number of rounds pbkdf = 100, is the strong of cypher.</remarks>
    public static string EncryptStrongAes(string textToEncrypt, string password)
    {
      if (string.IsNullOrEmpty(textToEncrypt)) throw new ArgumentNullException("textToEncrypt");
      if (password == null) throw new ArgumentNullException("password");
      //
      var passwordBytes = ByteHelper.GetBytes(password);
      var aes = new Aes();
      //
      using (var rngCsp = new RNGCryptoServiceProvider())
      {
        var salt = new byte[32];
        rngCsp.GetBytes(salt);
        //
        var compressed = GzipHelper.Compress(ByteHelper.GetBytes(textToEncrypt));
        var encrpytedMessage = aes.Encrypt(compressed, passwordBytes, salt, 100);
        var fullMessage = ByteHelper.Combine(salt, encrpytedMessage);
        //
        return Convert.ToBase64String(fullMessage);
      }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="textToDecrypt"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <remarks>Number of rounds pbkdf = 100, must be the same.</remarks>
    public static string DecryptStrongAes(string textToDecrypt, string password)
    {
      if (string.IsNullOrEmpty(textToDecrypt)) throw new ArgumentNullException("textToDecrypt");
      if (password == null) throw new ArgumentNullException("password");
      //
      var passwordBytes = ByteHelper.GetBytes(password);
      var aes = new Aes();
      var textToDecryptBytes = Convert.FromBase64String(textToDecrypt);
      var salt = ByteHelper.CreateSpecialByteArray(32);
      var message = ByteHelper.CreateSpecialByteArray(textToDecryptBytes.Length - 32);
      Buffer.BlockCopy(textToDecryptBytes, 0, salt, 0, 32);
      Buffer.BlockCopy(textToDecryptBytes, 32, message, 0, textToDecryptBytes.Length - 32);
      //
      var deCompressed = GzipHelper.Decompress(aes.Decrypt(message, passwordBytes, salt, 100));
      //
      return ByteHelper.GetString(deCompressed);
    }
  }
}


