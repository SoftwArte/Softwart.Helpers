using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Pluto.Tools
{
	public class LinqToXml
	{
		/// <summary>
		/// Obtiene el valor de un node Xml y devuelve un string con el valor, devolverá una cadena vacio si el nodo es nulo.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static string GetTextFromXElement( XElement node )
		{
			return node != null ? node.Value : string.Empty;
		}
		/// <summary>
		/// Return string value from attribute if its not null else return default value.
		/// </summary>
		/// <param name="attribute"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static string GetTextFromAttribute( XAttribute attribute, string defaultValue )
		{
			return attribute != null ? attribute.Value : defaultValue;
		}
		//
		public static int GetIntFromAttribute( XAttribute node, int defaultValue )
		{
			return node != null ? Convert.ToInt32( node.Value ) : defaultValue;
		}

		public static long GetLongFromAttribute( XAttribute node, int defaultValue )
		{
			return node != null ? Convert.ToInt64( node.Value ) : defaultValue;
		}

		/// <summary>
		/// Obtiene el valor booleano de un attributo, si el atributo no existe se devuelve false.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static bool GetBoolFromAttribute( XAttribute node )
		{
			return node != null && Convert.ToBoolean( node.Value );
		}
	}
}
