// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetStructValue<T> : Action, ISetupable<T, BlackboardPropertyName>, ISetupable<T, string>
		where T : struct
	{
		[BehaviorInfo] private T m_value;
		[BehaviorInfo] private BlackboardPropertyName m_propertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(T value, [NotNull] string propertyName)
		{
			Setup(value, new BlackboardPropertyName(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_propertyName, m_value);
			return Status.Success;
		}
	}
}
