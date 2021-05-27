// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class PlayDelayedAudioSource : Action,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_audioPropertyName;
		[BehaviorInfo] private float m_delay;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName audioPropertyName, float delay)
		{
			SetupInternal(audioPropertyName, delay);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string audioPropertyName, float delay)
		{
			SetupInternal(new BlackboardPropertyName(audioPropertyName), delay);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName audioPropertyName, float delay)
		{
			m_audioPropertyName = audioPropertyName;
			m_delay = delay;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_audioPropertyName, out AudioSource audio) & audio != null)
			{
				audio.PlayDelayed(m_delay);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
