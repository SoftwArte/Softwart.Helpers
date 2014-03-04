/************************************************
 *	Clone helper		
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/
 
namespace Softwarte.Helpers
{
	using System;
	using System.IO;
	using System.Runtime.Serialization;
	using System.Runtime.Serialization.Formatters.Binary;
	using System.Xml;
	public static class ClonerHelper
	{
		/// <summary> 
		/// Perform a deep Copy of the object.  Use a binayformatter with ISerializable objects
		/// </summary> 
		/// <typeparam name="T">The type of object being copied.</typeparam> 
		/// <param name="source">The object instance to copy.</param> 
		/// <returns>The copied object.</returns> 
		public static T BinaryClone<T>(T source)
			where T: class, ISerializable, new()
		{
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
		/// <summary>
		/// Perform a deepclone of and object with datagraph. Use DataContractSerializer and the object type must be decorated with DataContractAtrribute.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static T DataClone<T>(T source)
				where T : class, new()
		{
			//Checks
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
