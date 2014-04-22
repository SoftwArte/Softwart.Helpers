/************************************************
 *	Xml helper classes			
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/

namespace Softwarte.Helpers
{
    using System;
    using System.Xml.Linq;
	public class LinqToXmlHelper
	{
		/// <summary>
		/// Return a string with the value of node. Return a empty string if node value is null.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static string GetTextFromXElement( XElement node )
		{
			return node != null ? node.Value : string.Empty;
		}
		/// <summary>
		/// Return string value from attribute Return default value if attribute value is null.
		/// </summary>
		/// <param name="attribute"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static string GetTextFromAttribute( XAttribute attribute, string defaultValue )
		{
			return attribute != null ? attribute.Value : defaultValue;
		}
		/// <summary>
		/// Retunr a int value from attribute. Return default value passed if attribute value is null.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static int GetIntFromAttribute( XAttribute node, int defaultValue )
		{
			return node != null ? Convert.ToInt32( node.Value ) : defaultValue;
		}
        /// <summary>
        /// Return a long value from attribute. Return a default value if it is null.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public static long GetLongFromAttribute( XAttribute node, int defaultValue )
		{
			return node != null ? Convert.ToInt64( node.Value ) : defaultValue;
		}

		/// <summary>
		/// Return a boolean value from an attribute. Return false if node value is null.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static bool GetBoolFromAttribute( XAttribute node )
		{
			return node != null && Convert.ToBoolean( node.Value );
		}
	}
}
