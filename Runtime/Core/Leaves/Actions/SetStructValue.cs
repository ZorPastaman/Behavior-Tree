// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
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
		void ISetupable<T, BlackboardPropertyName>.Setup(T value, BlackboardPropertyName propertyName)
		{
			SetupInternal(value, propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<T, string>.Setup(T value, string propertyName)
		{
			SetupInternal(value, new BlackboardPropertyName(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_propertyName, m_value);
			return Status.Success;
		}
	}
}
