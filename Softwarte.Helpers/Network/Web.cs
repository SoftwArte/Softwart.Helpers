/************************************************
 *	Web helper classes			
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/


namespace Softwarte.Helpers
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.IO;
  using System.Net;
  using System.Text;

  public enum HttpMethodEnum
  {
    GET, POST
  }

  public class WebHelper
  {
    /// <summary>
    /// Convert a string dictionary in a WebHeaderCollection, dictionary key is the header name.
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    private static WebHeaderCollection ParseHeadersFromStrings(Dictionary<string, string> headers)
    {
      var headerCollection = new WebHeaderCollection();
      foreach (var header in headers)
      {
        headerCollection.Add(header.Key, header.Value);
      }
      return headerCollection;
    }
    /// <summary>
    ///	Download a web page as string using WebClient class, better support for encodings. Support methods GET and POST, configure http headers, and request body.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string GetWebPage(string url, HttpMethodEnum httpMethod = HttpMethodEnum.GET, Dictionary<string, string> headers = null, string body = "")
    {
      WebClient client = new WebClient();
      //
      if (httpMethod == HttpMethodEnum.GET)
      {
        //Add headers is passed
        if (headers != null) client.Headers = ParseHeadersFromStrings(headers);
        //Return a page using GET.
        return client.DownloadString(url);
      }
      else
      {
        //Add headers is passed
        if (headers != null) client.Headers = ParseHeadersFromStrings(headers);

        //Return a page using POST. A content-type header at least in needed.
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        return Encoding.UTF8.GetString(client.UploadData(url, "POST", Encoding.UTF8.GetBytes(body)));
      }
    }

    /// <summary>
    /// Download a web page as stream using WebRequest class, is neccesary read the strema to return the content as a string.
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
      while ((line = response.ReadLine()) != null)
      {
        if (line.Length > 0)
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
      using (var client = new WebClient())
      {
        try
        {
          return client.DownloadData(url);
        }
        catch (Exception ex)
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
      using (var client = new WebClient())
      {
        byte[] resource = client.DownloadData(url);
        using (var str = new MemoryStream())
        {
          str.Write(resource, 0, resource.Length);
          return Image.FromStream(str);
        }
      }

    }
    /// <summary>
    /// Build an absolute url with a base url and a url part.
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
