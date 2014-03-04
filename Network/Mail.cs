/************************************************
 *	Mail helper classes			
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/

namespace Softwarte.Helpers
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Net.Mail;
	using System.Xml;
	public class MailHelper
	{
		/// <summary>
		/// Send a simple mail message to a single mail address. Smtp configuration must exist in config file.
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


