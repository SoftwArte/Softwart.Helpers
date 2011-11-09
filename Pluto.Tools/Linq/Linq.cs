/************************************************
 *	Linq helper functions class			
 *	Programmed by: Rafael Hernández							
 *	Version: 1.1												
 * **********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Pluto.Tools
{
	public static class LinqHelper
	{
		// Ayuda a construir un arbol de expresion lambda.
		public static Expression<Func<T, Result>> Expr<T, Result>( Expression<Func<T, Result>> f )
		{
			return f;
		}
		// Ayuda a contruir un delegado lambda.
		public static Func<T, Result> Func<T, Result>( Func<T, Result> f )
		{
			return f;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public static class PredicateBuilder
	{
		#region Public Members
		public static Expression<Func<T, bool>> True<T>( ) { return f => true; }
		public static Expression<Func<T, bool>> False<T>( ) { return f => false; }
		public static Expression<Func<T, bool>> Or<T>( this Expression<Func<T, bool>> expr1,
																																												Expression<Func<T, bool>> expr2 )
		{
			var invokedExpr = Expression.Invoke( expr2, expr1.Parameters.Cast<Expression>( ) );
			return Expression.Lambda<Func<T, bool>>
								( Expression.OrElse( expr1.Body, invokedExpr ), expr1.Parameters );
		}
		public static Expression<Func<T, bool>> And<T>( this Expression<Func<T, bool>> expr1,
																																													Expression<Func<T, bool>> expr2 )
		{
			var invokedExpr = Expression.Invoke( expr2, expr1.Parameters.Cast<Expression>( ) );
			return Expression.Lambda<Func<T, bool>>
								( Expression.AndAlso( expr1.Body, invokedExpr ), expr1.Parameters );
		}
		#endregion

		//Samples
		//var Filtro = PredicateBuilder.True<Oferta>( );
		//Filtro = Filtro.And( p => p.Nombre.Contains( "ofv" ) );
		//Filtro = Filtro.And( p => p.FlagActivado == true );
		//var ListaOfertas = DataOfertas.GetByFilter( Filtro );
	}
}
