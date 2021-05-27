// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class PlayOneShotAudioSourceVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_clipPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_audioPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName clipPropertyName, BlackboardPropertyName audioPropertyName)
		{
			SetupInternal(clipPropertyName, audioPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string clipPropertyName, string audioPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(clipPropertyName), new BlackboardPropertyName(audioPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName clipPropertyName, BlackboardPropertyName audioPropertyName)
		{
			m_clipPropertyName = clipPropertyName;
			m_audioPropertyName = audioPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_clipPropertyName, out AudioClip clip) &
				blackboard.TryGetClassValue(m_audioPropertyName, out AudioSource audio) & audio != null)
			{
				audio.PlayOneShot(clip);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
