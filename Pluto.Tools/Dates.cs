// -----------------------------------------------------------------------
// <copyright file="Dates.cs" company="">
// DOC: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Pluto.Tools
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Helper class to dates operations.
	/// </summary>
	public class Dates
	{
		public static int GetDatesDiffInDays(DateTime firstDate, DateTime lastDate)
		{
			return (lastDate - firstDate).Days;
		}


	}

}
