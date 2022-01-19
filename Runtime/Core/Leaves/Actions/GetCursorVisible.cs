// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Gets <see cref="Cursor.visible"/> and sets it into the <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	/// Always returns <see cref="Status.Success"/> in its tick.
	/// </para>
	/// <para>
	/// The property name for the result of type <see cref="bool"/> is set in the setup method.
	/// </para>
	/// </summary>
	public sealed class GetCursorVisible : Action, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_visiblePropertyName;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName visiblePropertyName)
		{
			SetupInternal(visiblePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string visiblePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(visiblePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName visiblePropertyName)
		{
			m_visiblePropertyName = visiblePropertyName;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_visiblePropertyName, Cursor.visible);
			return Status.Success;
		}
	}
}
