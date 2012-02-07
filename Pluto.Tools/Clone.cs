/************************************************
 *	Clone functions class			
 *	Programmed by: Rafael Hernández										
 * **********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace Pluto.Tools
{
	/// <summary>2 métodos deep-clone, uno binario para ISerializables y otro XML basado en DataContractSerializer para tipos marcados con el
	/// atributo [DataContractAttribute()]</summary>

	public static class Cloner
	{
		/// <summary> 
		/// Perform a deep Copy of the object. 
		/// </summary> 
		/// <typeparam name="T">The type of object being copied.</typeparam> 
		/// <param name="source">The object instance to copy.</param> 
		/// <returns>The copied object.</returns> 
		public static T BinaryClone<T>(T source)
		{
			if(!typeof(T).IsSerializable)
			{
				throw new ArgumentException("The type must be serializable.", "source");
			}

			// Don't serialize a null object, simply return the default for that object 
			if(object.ReferenceEquals(source, null))
			{
				return default(T);
			}

			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			using(stream)
			{
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
		public static T DataClone<T>(T source)
				where T : class, new()
		{
			if(typeof(T).GetCustomAttributes(typeof(DataContractAttribute), false) == null)
			{
				throw new ArgumentException("The type must be decorate with DataContractAttribute.", "source");
			}

			// Don't serialize a null object, simply return the default for that object 
			if(object.ReferenceEquals(source, null))
			{
				return default(T);
			}

			using(var MemStream = new MemoryStream())
			{
				var Dct = new NetDataContractSerializer();
				using(var XmlDic = XmlDictionaryWriter.CreateBinaryWriter(MemStream))
				{
					Dct.WriteObject(XmlDic, source);
				}
				//
				T Tmp = null;
				var MemStream2 = MemStream.ToArray();
				using(var XmlDicR = XmlDictionaryReader.CreateBinaryReader(MemStream2, XmlDictionaryReaderQuotas.Max))
				{
					Tmp = (T)Dct.ReadObject(XmlDicR);
				}
				return Tmp;
			}

		}
	}
}
