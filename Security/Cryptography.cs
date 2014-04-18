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
    private const string KEY = "@yKDdW!SxrfWyYAWcG&Dtx&XFHdN^mNSqE%X8fUSvFbb4!2hHP";
    /// <summary>
    /// Encode a string with DES method. Key is embbeded.
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public static string EncryptDes(string texto)
    {
      //DES Crypter
      //Inicializa las claves utilizadas para la Encriptación.
      string keyValue = KEY;
      Byte[] Vector = { 128, 33, 52, 4, 9, 16, 79, 201, 68, 1, 55, 233, 156, 89, 17, 4 };
      Byte[] Key = Encoding.UTF8.GetBytes(keyValue);
      Byte[] TextoByte = Encoding.UTF8.GetBytes(texto);

      using (var DCryp = new DESCryptoServiceProvider())
      {
        using (MemoryStream MemStr = new MemoryStream())
        {
          //Realiza la encriptación.
          using (CryptoStream CrypStr = new CryptoStream(MemStr, DCryp.CreateEncryptor(Key, Vector),
                     CryptoStreamMode.Write))
          {
            CrypStr.Write(TextoByte, 0, TextoByte.Length);
            //Convierte el array encriptado en una cadena codificada en Base64.
            string Resultado = Convert.ToBase64String(MemStr.ToArray());
            return Resultado;
          }
        }
      }
    }
    /// <summary>
    /// Decode a string encoded with DES. Key is embeded.
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public static string DecryptDes(string texto)
    {
      //Inicializa las claves utilizadas para la desencriptación. Deben ser los mismos que al 
      //encriptar.
      string keyValue = KEY;
      Byte[] Vector = { 128, 33, 52, 4, 9, 16, 79, 201, 68, 1, 55, 233, 156, 89, 17, 4 };
      Byte[] Key = Encoding.UTF8.GetBytes(keyValue);
      Byte[] TextoByte = Encoding.UTF8.GetBytes(texto);

      using (var DCryp = new DESCryptoServiceProvider())
      {
        using (MemoryStream MemStr = new MemoryStream())
        {
          //Realiza la encriptación.
          using (CryptoStream CrypStr = new CryptoStream(MemStr, DCryp.CreateDecryptor(Key, Vector),
                      CryptoStreamMode.Write))
          {
            CrypStr.Write(TextoByte, 0, TextoByte.Length);
            //Devuelve la cadena resultante.
            string Resultado = Encoding.UTF8.GetString(MemStr.ToArray());
            return Resultado;
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

  }
}
