// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;

namespace Zor.BehaviorTree.EditorTools
{
	[InitializeOnLoad]
	public static class SerializedBehaviorsCollection
	{
		private static readonly Dictionary<Type, Type> s_serializedBehaviors;

		static SerializedBehaviorsCollection()
		{
			Type[] serializedBehaviorTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
					where !domainAssembly.IsDynamic
					from assemblyType in domainAssembly.GetExportedTypes()
					where !assemblyType.IsAbstract && !assemblyType.IsGenericType
						&& typeof(SerializedBehavior_Base).IsAssignableFrom(assemblyType)
					select assemblyType)
				.ToArray();

			int count = serializedBehaviorTypes.Length;
			s_serializedBehaviors = new Dictionary<Type, Type>(count);

			for (int i = 0; i < count; ++i)
			{
				Type serializedBehaviorType = serializedBehaviorTypes[i];
				Type behaviorType = ExtractBehaviorType(serializedBehaviorType);

				if (behaviorType != null)
				{
					s_serializedBehaviors[behaviorType] = serializedBehaviorType;
				}
			}
		}

		[NotNull, Pure]
		public static Type[] GetBehaviorTypes()
		{
			return s_serializedBehaviors.Keys.ToArray();
		}

		[Pure]
		public static bool TryGetSerializedBehaviorType([NotNull] Type behaviorType, out Type serializedBehaviorType)
		{
			return s_serializedBehaviors.TryGetValue(behaviorType, out serializedBehaviorType);
		}

		[CanBeNull, Pure]
		private static Type ExtractBehaviorType([NotNull] Type serializedBehaviorType)
		{
			while (serializedBehaviorType != null && serializedBehaviorType != typeof(object))
			{
				if (serializedBehaviorType.IsGenericType &&
					serializedBehaviorType.GetGenericTypeDefinition() == typeof(SerializedBehavior<>))
				{
					return serializedBehaviorType.GetGenericArguments()[0];
				}

				serializedBehaviorType = serializedBehaviorType.BaseType;
			}

			return null;
		}
	}
}
