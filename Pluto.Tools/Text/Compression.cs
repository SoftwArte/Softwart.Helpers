/*
*
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;


namespace Pluto.Tools.Text
{
    /// <summary>
    /// Class container of compression functions.
    /// </summary>
    public class Compression
    {
        /// <summary>
        /// Comprime a string and return an equivalente compresed array of bytes.
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
        /// Decompress an array of compresed bytes and return a string.
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


