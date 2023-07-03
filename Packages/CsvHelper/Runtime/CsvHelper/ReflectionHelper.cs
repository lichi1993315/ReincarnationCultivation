﻿// Copyright 2009-2020 Josh Close and Contributors
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CsvHelper.Configuration;

namespace CsvHelper
{
	/// <summary>
	/// Common reflection tasks.
	/// </summary>
	internal static class ReflectionHelper
	{
		/// <summary>
		/// Gets the <see cref="PropertyInfo"/> from the type where the property was declared.
		/// </summary>
		/// <param name="type">The type the property belongs to.</param>
		/// <param name="property">The property to search.</param>
		/// <param name="flags">Flags for how the property is retrieved.</param>
		public static PropertyInfo GetDeclaringProperty(Type type, PropertyInfo property, BindingFlags flags)
		{
			if (property.DeclaringType != type)
			{
				var declaringProperty = property.DeclaringType.GetProperty(property.Name, flags);
				return GetDeclaringProperty(property.DeclaringType, declaringProperty, flags);
			}

			return property;
		}

		/// <summary>
		/// Walk up the inheritance tree collecting properties. This will get a unique set or properties in the
		/// case where parents have the same property names as children.
		/// </summary>
		/// <param name="type">The <see cref="Type"/> to get properties for.</param>
		/// <param name="flags">The flags for getting the properties.</param>
		/// <param name="overwrite">If true, parent class properties that are hidden by `new` child properties will be overwritten.</param>
		public static List<PropertyInfo> GetUniqueProperties(Type type, BindingFlags flags, bool overwrite = false)
		{
			var properties = new Dictionary<string, PropertyInfo>();

			flags |= BindingFlags.DeclaredOnly;
			var currentType = type;
			while (currentType != null)
			{
				var currentProperties = currentType.GetProperties(flags);
				foreach (var property in currentProperties)
				{
					if (!properties.ContainsKey(property.Name) || overwrite)
					{
						properties[property.Name] = property;
					}
				}

				currentType = currentType.BaseType;
			}

			return properties.Values.ToList();
		}

		/// <summary>
		/// Gets the property from the expression.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">The expression.</param>
		/// <returns>The <see cref="PropertyInfo"/> for the expression.</returns>
		public static MemberInfo GetMember<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			var member = GetMemberExpression(expression.Body).Member;
			var property = member as PropertyInfo;
			if (property != null)
			{
				return property;
			}

			var field = member as FieldInfo;
			if (field != null)
			{
				return field;
			}

			throw new ConfigurationException($"'{member.Name}' is not a member.");
		}

		/// <summary>
		/// Gets the member inheritance chain as a stack.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">The member expression.</param>
		/// <returns>The inheritance chain for the given member expression as a stack.</returns>
		public static Stack<MemberInfo> GetMembers<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			var stack = new Stack<MemberInfo>();

			var currentExpression = expression.Body;
			while (true)
			{
				var memberExpression = GetMemberExpression(currentExpression);
				if (memberExpression == null)
				{
					break;
				}

				stack.Push(memberExpression.Member);
				currentExpression = memberExpression.Expression;
			}

			return stack;
		}

		private static MemberExpression GetMemberExpression(Expression expression)
		{
			MemberExpression memberExpression = null;
			if (expression.NodeType == ExpressionType.Convert)
			{
				var body = (UnaryExpression)expression;
				memberExpression = body.Operand as MemberExpression;
			}
			else if (expression.NodeType == ExpressionType.MemberAccess)
			{
				memberExpression = expression as MemberExpression;
			}

			return memberExpression;
		}
	}
}
