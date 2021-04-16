// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class RemoveClassValue<T> : Action,
		ISetupable<BlackboardPropertyName>, ISetupable<string>
		where T : class
	{
		private BlackboardPropertyName m_propertyName;

		public void Setup(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		public void Setup(string propertyName)
		{
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.RemoveObject<T>(m_propertyName);
			return Status.Success;
		}
	}
}
