// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class CopyStructValue<T> : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
		where T : struct
	{
		[BehaviorInfo] private BlackboardPropertyName m_sourcePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_destinationPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(BlackboardPropertyName sourcePropertyName, BlackboardPropertyName destinationPropertyName)
		{
			m_sourcePropertyName = sourcePropertyName;
			m_destinationPropertyName = destinationPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(string sourcePropertyName, string destinationPropertyName)
		{
			Setup(new BlackboardPropertyName(sourcePropertyName), new BlackboardPropertyName(destinationPropertyName));
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_sourcePropertyName, out T value))
			{
				blackboard.SetStructValue(m_destinationPropertyName, value);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
