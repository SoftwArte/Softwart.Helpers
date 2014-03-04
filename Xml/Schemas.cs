/************************************************
 *	Xml schemas helper classes			
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/

namespace Softwarte.Helpers
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
	public class SchemaHelper
	{
		/// <summary>
		/// Valida un fragmento Xml contra un esquema residente en un fichero.
		/// </summary>
		/// <param name="value">Fragmento Xml.</param>
		/// <param name="schemaFullPathName">Ruta absoluta completa del esquema.</param>
		public static bool SchemaValidator(XElement value, string schemaFullPathName)
		{
			//Carga el esquema de validación.
			XmlSchemaSet SchemaSet = new XmlSchemaSet();
			SchemaSet.Add(null, schemaFullPathName);
			SchemaSet.Compile();
			//Incrusta el fragmento en un documento para validar.
			XDocument TmpDoc = new XDocument();
			TmpDoc.Add(value);
			TmpDoc.Validate(SchemaSet, null);
			return true;
		}
		/// <summary>
		/// Valida un fragmento Xml contra un esquema como XmlSchema.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="schema"></param>
		/// <returns></returns>
		public static bool SchemaValidator(XElement value, XmlSchema schema)
		{
			//Carga el esquema de validación.
			XmlSchemaSet SchemaSet = new XmlSchemaSet();
			SchemaSet.Add(schema);
			SchemaSet.Compile();
			//Incrusta el fragmento en un documento para validar.
			XDocument TmpDoc = new XDocument();
			TmpDoc.Add(value);
			TmpDoc.Validate(SchemaSet, null);
			return true;
		}
		/// <summary>
		/// Validate an xml fragment against a xmlSchema from strings.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="schemaDefinition"></param>
		/// <returns></returns>
		public static bool SchemaValidator(string value, string schemaDefinition)
		{
			//Prepare schema set to validate against.
			XmlSchemaSet SchemaSet = new XmlSchemaSet();
			try
			{
				using(var str = new StringReader(schemaDefinition))
				{
					using(var sch = XmlReader.Create(str, new XmlReaderSettings { IgnoreWhitespace = true }))
					{
						var schema = XmlSchema.Read(sch, null);
						SchemaSet.Add(schema);
						SchemaSet.Compile();
						//Incrusta el fragmento en un documento para validar.
						XDocument TmpDoc = new XDocument();
						TmpDoc.Add(value);
						TmpDoc.Validate(SchemaSet, null);
					}

				}
				return true;
			}
			catch
			{
				throw;
			}
		}
	}

}
