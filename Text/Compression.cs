/************************************************
 *	Compresion helper classes			
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/

namespace Softwarte.Helpers
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    /// <summary>
    /// Class of compression functions.
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// Compress a string and return a compresed array of bytes.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Byte[] ZipString(string source)
        {
            var sourceByteArray = Encoding.UTF8.GetBytes(source);
            //
            using (var mstr = new MemoryStream())
            {
                using (var ds = new DeflateStream(mstr, CompressionMode.Compress))
                {
                    ds.Write(sourceByteArray, 0, sourceByteArray.Length);
                }
                return mstr.ToArray();
            }
        }
        /// <summary>
        /// Decompress an array of bytes and return a string.
        /// </summary>
        /// <param name="compresedSource"></param>
        /// <returns></returns>
        public static string UnZipString(byte[] compresedSource)
        {
            using (var mstr = new MemoryStream(compresedSource))
            {
                using (var ds = new DeflateStream(mstr, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(ds, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}


