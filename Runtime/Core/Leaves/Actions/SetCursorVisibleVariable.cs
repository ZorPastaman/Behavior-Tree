// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetCursorVisibleVariable : Action, ISetupable<BlackboardPropertyName>, ISetupable<string>
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

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_visiblePropertyName, out bool visible))
			{
				Cursor.visible = visible;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
