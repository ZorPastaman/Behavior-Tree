// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsAudioSourcePlaying : Condition, ISetupable<BlackboardPropertyName>, ISetupable<string>
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

		protected override unsafe Status Execute()
		{
			if (blackboard.TryGetClassValue(m_audioPropertyName, out AudioSource audio) & audio != null)
			{
				Status* results = stackalloc Status[] {Status.Failure, Status.Success};
				bool isPlaying = audio.isPlaying;
				byte index = *(byte*)&isPlaying;

				return results[index];
			}

			return Status.Error;
		}
	}
}
