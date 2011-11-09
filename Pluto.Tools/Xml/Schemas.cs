/************************************************
 *	Validators functions	
 *	Programmed by: Rafael Hernández							
 *	Version: 1.1 												
 * **********************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Pluto.Tools
{
	public class SchemaTools
	{
		/// <summary>
		/// Valida un fragmento Xml contra un esquema residente en un fichero.
		/// </summary>
		/// <param name="value">Fragmento Xml.</param>
		/// <param name="schemaFullPathName">Ruta absoluta completa del esquema.</param>
		public static bool SchemaValidator( XElement value, string schemaFullPathName )
		{
			//Carga el esquema de validación.
			XmlSchemaSet SchemaSet = new XmlSchemaSet( );
			SchemaSet.Add( null, schemaFullPathName );
			SchemaSet.Compile( );
			//Incrusta el fragmento en un documento para validar.
			XDocument TmpDoc = new XDocument( );
			TmpDoc.Add( value );
			TmpDoc.Validate( SchemaSet, null );
			return true;
		}
		/// <summary>
		/// Valida un fragmento Xml contra un esquema como XmlSchema.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="schema"></param>
		/// <returns></returns>
		public static bool SchemaValidator( XElement value, XmlSchema schema )
		{
			//Carga el esquema de validación.
			XmlSchemaSet SchemaSet = new XmlSchemaSet( );
			SchemaSet.Add( schema );
			SchemaSet.Compile( );
			//Incrusta el fragmento en un documento para validar.
			XDocument TmpDoc = new XDocument( );
			TmpDoc.Add( value );
			TmpDoc.Validate( SchemaSet, null );
			return true;
		}
		/// <summary>
		/// Validate an xml fragment against a xmlSchema from strings.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="schemaDefinition"></param>
		/// <returns></returns>
		public static bool SchemaValidator( string value, string schemaDefinition )
		{
			//Prepare schema set to validate against.
			XmlSchemaSet SchemaSet = new XmlSchemaSet( );
			try
			{
				using( var str = new StringReader( schemaDefinition ) )
				{
					using( var sch = XmlReader.Create( str, new XmlReaderSettings { IgnoreWhitespace = true } ) )
					{
						var schema = XmlSchema.Read( sch, null );
						SchemaSet.Add( schema );
						SchemaSet.Compile( );
						//Incrusta el fragmento en un documento para validar.
						XDocument TmpDoc = new XDocument( );
						TmpDoc.Add( value );
						TmpDoc.Validate( SchemaSet, null );
					}

				}
				return true;
			}
			catch( Exception ex )
			{
				throw;
			}
		}
	}

}
