// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;

namespace Zor.BehaviorTree.EditorTools
{
	/// <summary>
	/// Collection of all the serialized behaviors of type <see cref="SerializedBehavior_Base"/>.
	/// It's automatically filled.
	/// </summary>
	[InitializeOnLoad]
	public static class SerializedBehaviorsCollection
	{
		/// <summary>
		/// Dictionary of <see cref="Zor.BehaviorTree.Core.Behavior"/> to <see cref="SerializedBehavior_Base"/>.
		/// </summary>
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

		/// <summary>
		/// Gets all <see cref="Zor.BehaviorTree.Core.Behavior"/> types.
		/// </summary>
		/// <returns><see cref="Zor.BehaviorTree.Core.Behavior"/> types.</returns>
		[NotNull, Pure]
		public static Type[] GetBehaviorTypes()
		{
			return s_serializedBehaviors.Keys.ToArray();
		}

		/// <summary>
		/// Gets a serialized behavior type by a behavior type.
		/// </summary>
		/// <param name="behaviorType"><see cref="Zor.BehaviorTree.Core.Behavior"/> type.</param>
		/// <param name="serializedBehaviorType"><see cref="SerializedBehavior_Base"/> type.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="behaviorType"/> exists in the dictionary;
		/// <see langword="false"/> otherwise.
		/// </returns>
		[Pure]
		public static bool TryGetSerializedBehaviorType([NotNull] Type behaviorType, out Type serializedBehaviorType)
		{
			return s_serializedBehaviors.TryGetValue(behaviorType, out serializedBehaviorType);
		}

		/// <summary>
		/// Get a behavior type out of a serialized behavior type.
		/// </summary>
		/// <param name="serializedBehaviorType">Serialized behavior type.</param>
		/// <returns>Behavior type or <see langword="null"/> if the method can't extract a behavior.</returns>
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
