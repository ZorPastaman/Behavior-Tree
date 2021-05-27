// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class PauseAudioSource : Action, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_audioPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName audioPropertyName)
		{
			SetupInternal(audioPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string audioPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(audioPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName audioPropertyName)
		{
			m_audioPropertyName = audioPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_audioPropertyName, out AudioSource audio) & audio != null)
			{
				audio.Pause();
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
