/************************************************
 *	Regular expression helper classes			
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
    using System.Text.RegularExpressions;
	public class RegExpHelper
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
        /// <summary>
        /// Replace all occurrences of a pattern with a value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <param name="value"></param>
        /// <returns></returns>
		public static string Replace( string source, string pattern, string value )
		{
			return new Regex( pattern ).Replace( source, value );
		}
	}
}
