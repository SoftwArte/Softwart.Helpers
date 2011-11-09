/************************************************
 *	Cryptographic functions class			
 *	Programmed by: Rafael Hernández						
 *	Version: 1.2												
 * **********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Pluto.Tools
{
	public sealed partial class Hasher
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
		public static string CreateHash( string value, HashAlgorithm algorithm = HashAlgorithm.Sha512 )
		{
			switch( algorithm )
			{
				case HashAlgorithm.Sha256:
					throw new NotImplementedException( );
				case HashAlgorithm.Sha384:
					throw new NotImplementedException( );
				case HashAlgorithm.Sha512:
					return Convert.ToBase64String( SHA512.Create( ).ComputeHash( Encoding.UTF8.GetBytes( value ) ) );
				default:
					return Convert.ToBase64String( SHA512.Create( ).ComputeHash( Encoding.UTF8.GetBytes( value ) ) );
			}
		}
		/// <summary>
		/// Create a return a hash of byte array.
		/// </summary>
		/// <param name="byteArray"></param>
		/// <param name="algorithm"></param>
		/// <returns></returns>
		public static string CreateHash( byte[] byteArray, HashAlgorithm algorithm = HashAlgorithm.Sha512 )
		{
			switch( algorithm )
			{
				case HashAlgorithm.Sha256:
					throw new NotImplementedException( );
				case HashAlgorithm.Sha384:
					throw new NotImplementedException( );
				case HashAlgorithm.Sha512:
					return Convert.ToBase64String( SHA512.Create( ).ComputeHash( byteArray ) );
				default:
					return Convert.ToBase64String( SHA512.Create( ).ComputeHash( byteArray ) );
			}
		}
	}
	/// <summary>
	/// Esta clase contiene metodos para realizar operaciones de cifrado de cadenas y
	/// archivos. Todos los metodos son estáticos.
	/// </summary>
	/// <requirements>Ninguno</requirements>
	public sealed partial class Crypter
	{
		#region Ctors
		private Crypter( )
		{
		}
		#endregion

		#region Public members
		/// <summary>
		/// 	<para>Encripta una cadena de texto con el metodo de encriptación
		///     <strong>DES</strong>, las claves de encriptación están embebidas en el
		///     código.</para>
		/// 	<para>Sólo admite cadenas en formato UTF-8.</para>
		/// </summary>
		/// <returns>Cadena encriptada codificada en base 64.</returns>
		/// <remarks>
		/// 	<para>Usa una clave interna de 8 caracteres. Sólo admite cadenas en formato
		///     UTF-8.</para>
		/// </remarks>
		/// <example>
		/// 	<code lang="CS">
		/// String MyString = Crypter.EncryptDes(stringToEncrypt);
		/// </code>
		/// </example>
		/// <param name="texto">
		/// 	<para>Cadena o texto a encriptar en formato UTF-8. Todos los caracteres fuera del
		///     rango de UTF-8 serán trasladados a su equivalente dentro del rango.</para>
		/// </param>
		public static string EncryptDes( string texto )
		{
			//DES Crypter
			//Inicializa las claves utilizadas para la Encriptación.
			string keyValue = "Ch0RlitoMajareT0";
			Byte[] Vector = { 128, 33, 52, 4, 9, 16, 79, 201, 68, 1, 55, 233, 156, 89, 17, 4 };
			Byte[] Key = Encoding.UTF8.GetBytes( keyValue );
			Byte[] TextoByte = Encoding.UTF8.GetBytes( texto );

			DESCryptoServiceProvider DCryp = new DESCryptoServiceProvider( );
			using( MemoryStream MemStr = new MemoryStream( ) )
			{
				//Realiza la encriptación.
				using( CryptoStream CrypStr = new CryptoStream( MemStr, DCryp.CreateEncryptor( Key, Vector ),
									 CryptoStreamMode.Write ) )
				{
					CrypStr.Write( TextoByte, 0, TextoByte.Length );
					//Convierte el array encriptado en una cadena codificada en Base64.
					string Resultado = Convert.ToBase64String( MemStr.ToArray( ) );
					return Resultado;
				}
			}
		}
		/// <summary>
		/// 	<para>Desencripta una cadena de texto utilizando el metodo de encriptación
		///     <strong>DES</strong>, las claves de encriptación están embebidas en el
		///     código.</para>
		/// 	<para>La salida es en formato UTF-8.</para>
		/// </summary>
		/// <returns>Cadena desencriptada en formato UTF-8.</returns>
		/// <remarks><para>La clave interna de encriptación es de 8 caracteres fijos.</para></remarks>
		/// <example>
		/// 	<code lang="CS" title="[New Example]">
		/// 	</code>
		/// 	<code lang="CS">
		/// String MyString = Crypter.DecryptDes(stringToDecrypt);
		/// </code>
		/// </example>
		/// <param name="texto">Cadena a desencriptar.</param>
		public static string DecryptDes( string texto )
		{
			//Inicializa las claves utilizadas para la desencriptación. Deben ser los mismos que al 
			//encriptar.
			string keyValue = "Ch0RlitoMajareT0";
			Byte[] Vector = { 128, 33, 52, 4, 9, 16, 79, 201, 68, 1, 55, 233, 156, 89, 17, 4 };
			Byte[] Key = Encoding.UTF8.GetBytes( keyValue );
			Byte[] TextoByte = Encoding.UTF8.GetBytes( texto );

			DESCryptoServiceProvider DCryp = new DESCryptoServiceProvider( );
			using( MemoryStream MemStr = new MemoryStream( ) )
			{
				//Realiza la encriptación.
				using( CryptoStream CrypStr = new CryptoStream( MemStr, DCryp.CreateDecryptor( Key, Vector ),
										CryptoStreamMode.Write ) )
				{
					CrypStr.Write( TextoByte, 0, TextoByte.Length );
					//Devuelve la cadena resultante.
					string Resultado = Encoding.UTF8.GetString( MemStr.ToArray( ) );
					return Resultado;
				}
			}
		}
		/// <summary>
		/// 	<para>Desencripta un archivo utilizando el metodo "EncryptDes".</para>
		/// 	<para>La salida proporcionada es en formato UTF-8.</para>
		/// </summary>
		/// <returns>Contenido del archivo desencriptado, en formato UTF-8.</returns>
		/// <remarks><para>Devuelve el contenido desencriptado en formato UTF-8.</para></remarks>
		/// <example>
		/// 	<para></para>
		/// 	<code lang="CS">
		/// String MyString = Crypter.DecrypFileContent(RutaArchivo);
		/// </code>
		/// </example>
		/// <param name="filepath">Full path del archivo a encriptar.</param>
		public static string DecrypFileContent( string filepath )
		{
			using( var oFile = File.Open( filepath, FileMode.Open, FileAccess.Read ) )
			{
				using( var oRdr = new StreamReader( oFile ) )
				{
					string fileContent = oRdr.ReadToEnd( );
					string Resultado = Crypter.DecryptDes( fileContent );
					return Resultado;
				}

			}
		}
		/// <summary>
		/// 	<para>Encripta el contenido de un archivo especificado y devuelve como una cadena
		///     el resultado de la encriptación.</para>
		/// 	<para>Sólo admite formato de texto UTF-8.</para>
		/// </summary>
		/// <returns>Contenido del archivo encriptado codificado en base64.</returns>
		/// <example>
		/// 	<code lang="CS">
		/// String ContenidoEncriptado = Crypter.EncryptFileContent(RutaArchivo);
		/// </code>
		/// </example>
		/// <param name="filepath">Ruta absoluta completa del archivo a Encriptar.</param>
		public static string EncryptFileContent( string filepath )
		{
			using( var oFile = File.Open( filepath, FileMode.Open, FileAccess.Read ) )
			{
				using( var oRdr = new StreamReader( oFile ) )
				{
					string fileContent = oRdr.ReadToEnd( );
					string Resultado = Crypter.EncryptDes( fileContent );
					return Resultado;
				}
			}
		}
		#endregion
	}
}
