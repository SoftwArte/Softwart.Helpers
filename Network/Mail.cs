/*
*
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Xml;


namespace Pluto.Tools
{
	public class Mail
	{
		/// <summary>
		/// Send a simple mail message to a single mail address.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="subject"></param>
		/// <param name="address"></param>
		public static void SendMessage(string message, string subject, string address)
		{
			var mail = new MailMessage();

			mail.To.Add(new MailAddress(address));
			mail.Subject = subject;
			mail.Body = message;

			var cliente = new SmtpClient();
			cliente.Send(mail);
		}
	}
}


