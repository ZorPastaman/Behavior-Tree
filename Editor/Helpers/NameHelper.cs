// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Helpers
{
	/// <summary>
	/// Edits field names.
	/// </summary>
	internal static class NameHelper
	{
		/// <summary>
		/// Transforms a field name. The method removed _ or m_ at the start of the field name.
		/// </summary>
		/// <param name="fieldName">Field name.</param>
		/// <returns>Transformed field name.</returns>
		public static string GetNameForField([NotNull] string fieldName)
		{
			if (fieldName.StartsWith("m_"))
			{
				fieldName = fieldName.Substring(2);
			}
			else if (fieldName.StartsWith("_"))
			{
				fieldName = fieldName.Substring(1);
			}

			fieldName = char.ToUpper(fieldName[0]) + fieldName.Substring(1);

			return GetNameWithSpaces(fieldName);
		}

		/// <summary>
		/// Transforms a type name. The method inserts spaces before big letters except the first letter in the string.
		/// </summary>
		/// <param name="typeName">Type name.</param>
		/// <returns>Transformed type name.</returns>
		[NotNull]
		public static string GetNameWithSpaces([NotNull] string typeName)
		{
			return Regex.Replace(typeName, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
		}
	}
}
