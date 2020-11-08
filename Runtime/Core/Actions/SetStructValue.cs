// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Actions
{
	[UsedImplicitly, Preserve]
	public sealed class SetStructValue<T> : Behavior where T : struct
	{
		private readonly T m_value;
		private readonly BlackboardPropertyName m_propertyName;

		public SetStructValue(T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		public SetStructValue(T value, [NotNull] string propertyName)
		{
			m_value = value;
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_propertyName, m_value);
			return Status.Success;
		}
	}
}
