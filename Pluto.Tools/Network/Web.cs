using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using System.Xml.Linq;


namespace Pluto.Tools
{
	public class WebTools
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string GetPage( Uri url )
		{
			WebRequest request = WebRequest.Create( url );
			StreamReader response = new StreamReader( request.GetResponse( ).GetResponseStream( ) );
			StringBuilder sb = new StringBuilder( );
			string line;

			// Copy the response stream 
			while( ( line = response.ReadLine( ) ) != null )
			{
				if( line.Length > 0 )
					sb.Append( line );
			}

			response.Close( );
			return sb.ToString( );
		}
		/// <summary>
		/// Download and return a binary representation of a web resource as byte array.
		/// </summary>
		/// <returns></returns>
		public static byte[] GetWebResourceBinary( string url )
		{
			using( var client = new WebClient( ) )
			{
				return client.DownloadData( url );
			}


		}
		/// <summary>
		/// Download and return an image from url.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static Image GetWebImage( string url )
		{
			using( var client = new WebClient( ) )
			{
				byte[] resource = client.DownloadData( url );
				var str = new MemoryStream( );
				str.Write( resource, 0, resource.Length );
				return Image.FromStream( str );
			}

		}
		/// <summary>
		/// Compound an absolute url with a base url and a url part.
		/// </summary>
		/// <param name="baseUrl"></param>
		/// <param name="urlFragment"></param>
		/// <returns></returns>
		public static string GetAbsoluteUrl( string baseUrl, string urlFragment )
		{
			return new Uri( new Uri( baseUrl ), urlFragment ).ToString( );
		}
	}

}
