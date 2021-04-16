// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetClassValue<T> : Action,
		ISetupable<T, BlackboardPropertyName>, ISetupable<T, string>
		where T : class
	{
		[CanBeNull] private T m_value;
		private BlackboardPropertyName m_propertyName;

		public void Setup(T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		public void Setup(T value, string propertyName)
		{
			m_value = value;
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetClassValue(m_propertyName, m_value);
			return Status.Success;
		}
	}
}
