// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Text;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Helpers
{
	/// <summary>
	/// Helps to get a nice type name for ui.
	/// </summary>
	internal static class TypeHelper
	{
		/// <summary>
		/// Transforms a type name. The method inserts spaces between words and adds generic argument types.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <returns>Transformed type name.</returns>
		[NotNull]
		public static string GetUIName([NotNull] Type type)
		{
			if (!type.IsGenericType)
			{
				// "object" name not to confuse it with UnityEngine.Object.
				return type == typeof(object) ? "object" : NameHelper.GetNameWithSpaces(type.Name);
			}

			string genericName = type.Name;
			genericName = genericName.Substring(0, genericName.IndexOf("`", StringComparison.Ordinal));
			var stringBuilder = new StringBuilder(NameHelper.GetNameWithSpaces(genericName));
			stringBuilder.Append(" <");

			Type[] genericParameters = type.GetGenericArguments();

			for (int i = 0, count = genericParameters.Length; i < count; ++i)
			{
				stringBuilder.Append(GetUIName(genericParameters[i]));

				if (i < count - 1)
				{
					stringBuilder.Append(", ");
				}
			}

			stringBuilder.Append('>');

			return stringBuilder.ToString();
		}
	}
}
