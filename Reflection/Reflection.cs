/************************************************
 *	Reflection Helper class			
 *	Programmed by: Rafael Hernández							
 *	Version: 1.3						
 * **********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace Pluto.Tools
{
	/// <summary>
	/// Clase con métodos para la invocación dinámica de miembros de clases genericas por reflection.
	/// </summary>
	public class Reflector
	{
		public static Type GenericLinqFuncReflected(params Type[] parameters)
		{
			Type FuncOpen = typeof(Func<,>);
			Type FuncClose = FuncOpen.MakeGenericType(parameters);
			return FuncClose;
		}
		/// <summary>
		/// Devuelve el tipo cerrado de un tipo Func con los parametros declarados."/>
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static Type GenericLinqExpressionReflected(params Type[] parameters)
		{
			Type FuncOpen = typeof(Func<,>);
			Type FuncGenericClose = FuncOpen.MakeGenericType(parameters);
			Type ExpOpen = typeof(Expression<>);
			Type ExpClose = ExpOpen.MakeGenericType(FuncGenericClose);
			return ExpClose;
		}
		/// <summary>
		/// Devuelve el Type por su nombre largo incluido el namespace. Busca en el ensamblado predeterminado.
		/// </summary>
		/// <param name="typeLongName"></param>
		/// <returns></returns>
		public static Type GetTypeByName(string assemblyName, string typeLongName)
		{
			return Assembly.Load(assemblyName).GetType(typeLongName);
		}
		/// <summary>
		/// Devuelve el tipo del argumento generico del tipo declarado un una propiedad de una entidad.
		/// </summary>
		/// <param name="mainType"></param>
		/// <param name="propertyTypeName"></param>
		/// <returns></returns>
		public static Type GetSubyacentTypeOfProperty(Type mainType, string propertyTypeName)
		{
			return mainType.GetProperty(propertyTypeName).PropertyType.GetGenericArguments()[0];
		}
		/// <summary>
		/// Invoca un método estático miembro del tipo especificado.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static dynamic InvocadorSimpleEstatico(Type type, string methodName, Type[] parameters = null, Object[] values = null)
		{
			if(parameters != null)
			{
				MethodInfo M = type.GetMethod(methodName, parameters);
				return M.Invoke(null, values);
			}
			else
			{
				MethodInfo M = type.GetMethod(methodName);
				return M.Invoke(null, values);
			}
		}
		/// <summary>
		/// Invoca mediante reflection un método por su nombre. Es necesario indicar el tipo de objeto que posee el método, el tipo de este objeto, los tipos de los parametros y los valores a pasar al método.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="type"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static dynamic InvocadorSimple(Object instance, Type type, string methodName, Type[] parameters = null, Object[] values = null)
		{
			if(parameters != null)
			{
				MethodInfo M = type.GetMethod(methodName, parameters);
				return M.Invoke(instance, values);
			}
			else
			{
				MethodInfo M = type.GetMethod(methodName);
				return M.Invoke(instance, values);
			}
		}
		/// <summary>
		/// Invoca un metodo de un tipo generico, solo admite un parametro generico.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="genericParameter"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="values"></param>
		/// <returns>Devuelve un object, pero si internamente era una coleccion implementa IEnumerable.</returns>
		public static dynamic InvocadorGenerico(Type type, Type genericParameter, string methodName, Type[] parameters, Object[] values)
		{
			Type OpenType = type;
			Type CloseType = OpenType.MakeGenericType(genericParameter);
			//
			MethodInfo M = CloseType.GetMethod(methodName, parameters);
			Object Obj = Activator.CreateInstance(CloseType);
			//Devuelve un object con lo que devuelva e..l método.
			return M.Invoke(Obj, values);
		}
		/// <summary>
		/// Invoca un metodo generico con múltiples parametros genéricos.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="genericParameters"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static dynamic InvocadorGenericoMultiple(Type type, Type[] genericParameters, string methodName, Type[] parameters, Object[] values)
		{
			Type OpenType = type;
			Type CloseType = OpenType.MakeGenericType(genericParameters);
			//
			MethodInfo M = CloseType.GetMethod(methodName, parameters);
			Object Obj = Activator.CreateInstance(CloseType);
			//Devuelve un object con lo que devuelva el método.
			return M.Invoke(Obj, values);
		}
		/// <summary>
		/// Establece el valor de la propiedad de un tipo por reflection, acepta que el tipo de la propiedad sea Nullable generico.
		/// </summary>
		/// <param name="objToChange"></param>
		/// <param name="propertyName"></param>
		/// <param name="value"></param>
		public static void PropertyInfoSetter(object objToChange, string propertyName, object value)
		{
			Type FieldType = null;
			//Obtiene el tipo del propiedad para realizar una conversion si es necesario.															
			PropertyInfo Property = objToChange.GetType().GetProperty(propertyName);
			//Si el tipo de la propiedad es generico.
			if(Property.PropertyType.GetGenericArguments().Count() > 0)
			{
				//Obtiene el tipo argumento del tipo generico. Ex:Nullables
				FieldType = objToChange.GetType().GetProperty(propertyName).PropertyType.GetGenericArguments()[0];
			}
			else
			{
				//Obtiene en tipo no generico de la propiedad.
				FieldType = objToChange.GetType().GetProperty(propertyName).PropertyType;
			}
			//Comprueba el fieldType para hacer una conversion. Al soportar nullables, hay que tenerlo en cuenta al 
			//realizar conversiones. No se establecen propiedades si no nulos o vacios.
			//Ajusta a null si es un string vacio.
			if(value != null)
			{
				if(String.IsNullOrEmpty(value.ToString())) value = null;
			}
			switch(FieldType.Name)
			{
				case "String":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, value, null);
					}
					break;
				case "DateTime":
					if(value != null)
					{
						//Para remover las comillas pasadas en las fechas.
						var DateParsed = DateTime.Parse(value.ToString().Replace("\"", " "));
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToDateTime(DateParsed), null);
					}
					break;
				case "Boolean":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToBoolean(value), null);
					}
					break;
				case "Int16":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToInt16(value), null);
					}
					else
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, null, null);
					}
					break;
				case "Int32":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToInt32(value), null);
					}
					else
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, null, null);
					}
					break;
				case "Int64":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToInt64(value), null);
					}
					else
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, null, null);
					}
					break;
				case "Double":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToDouble(value), null);
					}
					break;
				case "Decimal":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToDecimal(value), null);
					}
					break;
				case "Single":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToSingle(value), null);
					}
					break;
				case "Byte":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Convert.ToByte(value), null);
					}
					break;
				case "Guid":
					if(value != null)
					{
						objToChange.GetType().GetProperty(propertyName).SetValue(objToChange, Guid.Parse(value.ToString()), null);
					}
					break;

			}
		}
		public static dynamic PropertyInfoGetter(object instance, string propertyName)
		{
			Type FieldType = null;
			//Obtiene el tipo del propiedad para realizar una conversion si es necesario.															
			PropertyInfo Prop = instance.GetType().GetProperty(propertyName);
			//Si el tipo de la propiedad es generico.
			if(Prop.PropertyType.GetGenericArguments().Count() > 0)
			{
				//Obtiene el tipo argumento del tipo generico. Ex:Nullables
				FieldType = Prop.PropertyType.GetGenericArguments()[0];
			}
			else
			{
				//Obtiene en tipo no generico de la propiedad.
				FieldType = Prop.PropertyType;
			}
			//Comprueba el fieldType para hacer una conversion, hay que comprobar la existencia de nulos
			//que no se pueden convertir.
			var PropValue = Prop.GetValue(instance, null);
			if(PropValue != null)
			{
				switch(FieldType.Name)
				{
					case "String":
						return PropValue.ToString();
					case "DateTime":
						return Convert.ToDateTime(PropValue);
					case "Boolean":
						return Convert.ToBoolean(PropValue);
					case "Int16":
						return Convert.ToInt16(PropValue);
					case "Int32":
						return Convert.ToInt32(PropValue);
					case "Int64":
						return Convert.ToInt64(PropValue);
					case "Double":
						return Convert.ToDouble(PropValue);
					case "Decimal":
						return Convert.ToDecimal(PropValue);
					case "Single":
						return Convert.ToSingle(PropValue);
					case "Byte":
						return Convert.ToByte(PropValue);
					case "default":
						return PropValue;
				}
			}
			return PropValue;
		}
	}
}
