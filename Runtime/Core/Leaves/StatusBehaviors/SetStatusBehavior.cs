// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.StatusBehaviors
{
	public sealed class SetStatusBehavior : StatusBehavior, ISetupable<Status>
	{
		[BehaviorInfo] private Status m_returnedStatus;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(Status returnedStatus)
		{
			m_returnedStatus = returnedStatus;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		protected override Status Execute()
		{
			return m_returnedStatus;
		}
	}
}
