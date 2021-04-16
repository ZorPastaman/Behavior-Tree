// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class RemoveStructValue<T> : Action,
		ISetupable<BlackboardPropertyName>, ISetupable<string>
		where T : struct
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
			blackboard.RemoveStruct<T>(m_propertyName);
			return Status.Success;
		}
	}
}
