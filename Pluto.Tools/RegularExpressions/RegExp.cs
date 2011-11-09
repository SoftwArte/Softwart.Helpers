/************************************************
 *	Regular expressions functions class			
 *	Programmed by: Rafael Hernández										
 * **********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Pluto.Tools
{
	public class RgExpression
	{
		/// <summary>
		/// Get the first ocurrence that validate the rule.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rule">Regular expression rule.</param>
		/// <returns></returns>
		public static string Extract( string source, string rule )
		{
			return new Regex( rule ).Match( source ).ToString( );
		}

		public static string Replace( string source, string pattern, string value )
		{
			return new Regex( pattern ).Replace( source, value );
		}
	}
}
