// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Helpers
{
	internal static class TypeHelper
	{
		[NotNull]
		public static string GetUIName([NotNull] Type type)
		{
			if (!type.IsGenericType)
			{
				return GetNameWithSpaces(type.Name);
			}

			string genericName = type.Name;
			genericName = genericName.Substring(0, genericName.IndexOf("`", StringComparison.Ordinal));
			var stringBuilder = new StringBuilder(GetNameWithSpaces(genericName));
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

		[NotNull]
		private static string GetNameWithSpaces([NotNull] string typeName)
		{
			return Regex.Replace(typeName, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
		}
	}
}
