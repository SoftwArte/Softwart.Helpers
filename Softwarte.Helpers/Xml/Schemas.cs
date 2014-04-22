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
		/// Validate a xml fragment against a xml schema from a file.
		/// </summary>
		/// <param name="value">Fragmento Xml.</param>
		/// <param name="schemaFullPathName">Ruta absoluta completa del esquema.</param>
		public static bool SchemaValidator(XElement value, string schemaFullPathName)
		{
            //Load schema in a schema set.
			XmlSchemaSet SchemaSet = new XmlSchemaSet();
			SchemaSet.Add(null, schemaFullPathName);
			SchemaSet.Compile();
			//Create a xml document to insert the fragment.
			XDocument TmpDoc = new XDocument();
			TmpDoc.Add(value);
			TmpDoc.Validate(SchemaSet, null);
			return true;
		}
		/// <summary>
		/// Validate a xml schema against a XmlSchema object.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="schema"></param>
		/// <returns></returns>
		public static bool SchemaValidator(XElement value, XmlSchema schema)
		{
            //Load schema in a schema set.
			XmlSchemaSet SchemaSet = new XmlSchemaSet();
			SchemaSet.Add(schema);
			SchemaSet.Compile();
            //Create a xml document to insert the fragment.
			XDocument TmpDoc = new XDocument();
			TmpDoc.Add(value);
			TmpDoc.Validate(SchemaSet, null);
			return true;
		}
		/// <summary>
		/// Validata a xml fragment against a xml schema from string.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="schemaDefinition"></param>
		/// <returns></returns>
		public static bool SchemaValidator(string value, string schemaDefinition)
		{
            //Load schema in a schema set.
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
                        //Create a xml document to insert the fragment.
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
