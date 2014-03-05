/************************************************
 *	Web helper classes			
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/


namespace Softwarte.Helpers
{
	using System;
	using System.Drawing;
	using System.IO;
	using System.Net;
	using System.Text;
	public class WebHelper
	{
    public enum HttpMethodEnum
    {
      GET, POST
    }
		/// <summary>
		///	Download a web page as string, better support for encodings. 
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string GetWebPage(string url, HttpMethodEnum httpMethod , string body = "")
		{
			WebClient client = new WebClient();
			//
			if(httpMethod == HttpMethodEnum.GET)
			{
				//Return a page using GET.
				return client.DownloadString(url);
			}
			else
			{
				//Return a page using POST.
				client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
				return Encoding.UTF8.GetString(client.UploadData(url, "POST", Encoding.UTF8.GetBytes(body)));
			}
		}

		/// <summary>
		/// Download a web page as stream, is neccesary read the strema to return the content as a string.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string GetPage(Uri url)
		{
			WebRequest request = WebRequest.Create(url);
			StreamReader response = new StreamReader(request.GetResponse().GetResponseStream());
			StringBuilder sb = new StringBuilder();
			string line;

			// Copy the response stream 
			while((line = response.ReadLine()) != null)
			{
				if(line.Length > 0)
					sb.Append(line);
			}

			response.Close();
			return sb.ToString();
		}
		/// <summary>
		/// Download and return a binary representation of a web resource as byte array.
		/// </summary>
		/// <remarks>if cant download by exception, return empty byte[] array. </remarks>
		/// <returns></returns>
		public static byte[] GetWebResourceBinary(string url)
		{
			using(var client = new WebClient())
			{
				try
				{
					return client.DownloadData(url);
				}
				catch(Exception ex)
				{
					return new byte[] { };
				}
			}


		}
		/// <summary>
		/// Download and return an image from url.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static Image GetWebImage(string url)
		{
			using(var client = new WebClient())
			{
				byte[] resource = client.DownloadData(url);
				using(var str = new MemoryStream())
				{
					str.Write(resource, 0, resource.Length);
					return Image.FromStream(str);
				}
			}

		}
		/// <summary>
		/// Compound an absolute url with a base url and a url part.
		/// </summary>
		/// <param name="baseUrl"></param>
		/// <param name="urlFragment"></param>
		/// <returns></returns>
		public static string GetAbsoluteUrl(string baseUrl, string urlFragment)
		{
			return new Uri(new Uri(baseUrl), urlFragment).ToString();
		}
	}

}
