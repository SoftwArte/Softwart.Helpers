/*
*
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace Pluto.Tools
{
	public class WebBrowse
	{
		private static WebBrowser client;

		public WebBrowse()
		{
			BrowserState = BrowserPageStateEnum.Ready;
			HttpMethod = "GET";
			Body = "";
		}

		public string SourceCode { get; set; }

		public HtmlDocument BrowserPage { get; set; }

		public BrowserPageStateEnum BrowserState { get; set; }

		public string Url { get; set; }

		public string HttpMethod { get; set; }

		public string Body { get; set; }

		//Browse a page.
		public void GetBrowseWebPage()
		{
			BrowserState = BrowserPageStateEnum.Downloading;
			client = new WebBrowser();
			client.Visible = true;
			client.ScriptErrorsSuppressed = true;
			client.AllowNavigation = true;
			client.DocumentCompleted += ClientOnDocumentCompleted;
			if(HttpMethod == "GET")
			{
				//Get a page using GET.
				client.Navigate(Url);
			}
			else
			{
				//Return a page using POST.
				client.Navigate(Url, "_self", Encoding.UTF8.GetBytes(Body), "Content-Type=pplication/x-www-form-urlencoded");
			}
		}
		//Handler.
		private void ClientOnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if(e.Url != null)
			{
				BrowserState = BrowserPageStateEnum.Completed;
				BrowserPage = client.Document;
				SourceCode = client.DocumentText;
			}
		}

		public enum BrowserPageStateEnum
		{
			Ready, Downloading, Completed
		}
	}
}


