/************************************************
 *	Dates helper		
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/

namespace Softwarte.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Helper class to dates operations.
	/// </summary>
	public class DatesHelper
	{
		/// <summary>
		/// Returns the difference between dates on days.
		/// </summary>
		/// <param name="firstDate"></param>
		/// <param name="lastDate"></param>
		/// <returns></returns>
		public static int GetDatesDiffInDays(DateTime firstDate, DateTime lastDate)
		{
			return (lastDate - firstDate).Days;
		}
		/// <summary>
		/// Returns ticks value of a date.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetDateTimeKey(DateTime value)
		{
			return value.Ticks.ToString();
		}

	}

}
