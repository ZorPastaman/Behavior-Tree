// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class RemoveClassValue<T> : Action, ISetupable<BlackboardPropertyName>, ISetupable<string>
		where T : class
	{
		[BehaviorInfo] private BlackboardPropertyName m_propertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName propertyName)
		{
			SetupInternal(propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string propertyName)
		{
			SetupInternal(new BlackboardPropertyName(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.RemoveObject<T>(m_propertyName);
			return Status.Success;
		}
	}
}
