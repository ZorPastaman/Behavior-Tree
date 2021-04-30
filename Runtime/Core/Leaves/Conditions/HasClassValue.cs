// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class HasClassValue<T> : Condition,
		ISetupable<BlackboardPropertyName>, ISetupable<string>
		where T : class
	{
		[BehaviorInfo] private BlackboardPropertyName m_propertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup([NotNull] string propertyName)
		{
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		[Pure]
		protected override unsafe Status Execute()
		{
			Status* results = stackalloc Status[] {Status.Failure, Status.Success};
			bool hasValue = blackboard.ContainsObjectValue<T>(m_propertyName);
			byte index = *(byte*)&hasValue;

			return results[index];
		}
	}
}
