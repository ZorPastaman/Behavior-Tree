// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Helpers
{
	internal static class NameHelper
	{
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

		[NotNull]
		public static string GetNameWithSpaces([NotNull] string typeName)
		{
			return Regex.Replace(typeName, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
		}
	}
}
