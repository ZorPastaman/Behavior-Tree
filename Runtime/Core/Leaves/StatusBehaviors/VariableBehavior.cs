// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.StatusBehaviors
{
	/// <summary>
	/// <para>
	/// Returns a variable <see cref="Status"/>.
	/// </para>
	/// <para>
	/// The property name of the returned <see cref="Status"/> is set in the setup method.
	/// </para>
	/// </summary>
	public sealed class VariableBehavior : StatusBehavior, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_variableName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName variableName)
		{
			SetupInternal(variableName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string variableName)
		{
			SetupInternal(new BlackboardPropertyName(variableName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName variableName)
		{
			m_variableName = variableName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool found = blackboard.TryGetStructValue(m_variableName, out Status variableStatus);
			return StateToStatusHelper.ConditionToStatus(found, Status.Error, variableStatus);
		}
	}
}
