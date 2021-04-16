// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Leaves.StatusBehaviors
{
	public sealed class SetStatusBehavior : StatusBehavior, ISetupable<Status>
	{
		private Status m_returnedStatus;

		public void Setup(Status returnedStatus)
		{
			m_returnedStatus = returnedStatus;
		}

		protected override Status Execute()
		{
			return m_returnedStatus;
		}
	}
}
