// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Actions
{
	[UsedImplicitly, Preserve]
	public sealed class RemoveStructValue<T> : Behavior where T : struct
	{
		private readonly BlackboardPropertyName m_propertyName;

		public RemoveStructValue(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		public RemoveStructValue([NotNull] string propertyName)
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