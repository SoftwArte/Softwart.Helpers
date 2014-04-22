/************************************************
 *	Reflection helper classes			
 *	Programmed by: Rafael Hernández
 *	Revision Date: 4/03/2014
 *	Version: 1.3												
 * **********************************************/

namespace Softwarte.Helpers
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	/// <summary>
	/// Functions to dinamically invocation of members by reflection.
	/// </summary>
	public class ReflectionHelper
	{
        /// <summary>
        /// Return a closed Func type with parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
		public static Type GenericLinqFuncReflected(params Type[] parameters)
		{
			Type FuncOpen = typeof(Func<,>);
			Type FuncClose = FuncOpen.MakeGenericType(parameters);
			return FuncClose;
		}
		/// <summary>
        /// Return a closed Expression type with parameters.
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
		/// Return a type by his name, assembly name is needed.
		/// </summary>
		/// <param name="typeLongName"></param>
		/// <returns></returns>
		public static Type GetTypeByName(string assemblyName, string typeLongName)
		{
			return Assembly.Load(assemblyName).GetType(typeLongName);
		}
		/// <summary>
        /// Return the type of a generic argument of the type of a property of the main type.
		/// </summary>
		/// <param name="mainType"></param>
		/// <param name="propertyTypeName"></param>
		/// <returns></returns>
		public static Type GetSubyacentTypeOfProperty(Type mainType, string propertyTypeName)
		{
			return mainType.GetProperty(propertyTypeName).PropertyType.GetGenericArguments()[0];
		}
		/// <summary>
		/// Invoke a static method of a type.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static dynamic InvokeStaticSimple(Type type, string methodName, Type[] parameters = null, Object[] values = null)
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
		/// Invoke a non static method of type, instance object is needed.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="type"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static dynamic InvokeSimple(Object instance, Type type, string methodName, Type[] parameters = null, Object[] values = null)
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
		/// Invoke a non static method of a genetic type with only one generic argument.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="genericParameter"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="values"></param>
		/// <returns>Return an object.</returns>
        /// <remarks>If the return object is a collection, implements IEnumerable</remarks>
		public static dynamic InvokeGenericSimple(Type type, Type genericParameter, string methodName, Type[] parameters, Object[] values)
		{
			Type OpenType = type;
			Type CloseType = OpenType.MakeGenericType(genericParameter);
			//
			MethodInfo M = CloseType.GetMethod(methodName, parameters);
			Object Obj = Activator.CreateInstance(CloseType);
			return M.Invoke(Obj, values);
		}
		/// <summary>
		/// Invoke a non static method of a generic type with multiple arguments.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="genericParameters"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static dynamic InvokeGeneric(Type type, Type[] genericParameters, string methodName, Type[] parameters, Object[] values)
		{
			Type OpenType = type;
			Type CloseType = OpenType.MakeGenericType(genericParameters);
			//
			MethodInfo M = CloseType.GetMethod(methodName, parameters);
			Object Obj = Activator.CreateInstance(CloseType);
			return M.Invoke(Obj, values);
		}
		/// <summary>
		/// Set the value of an object property. Accept nullable generic properties.
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
        /// <summary>
        /// Get the value of an object property. Accept nullable and generic properties.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
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
