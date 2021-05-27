// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetAudioSourcePlaying : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_audioPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName audioPropertyName, BlackboardPropertyName valuePropertyName)
		{
			SetupInternal(audioPropertyName, valuePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string audioPropertyName, string valuePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(audioPropertyName), new BlackboardPropertyName(valuePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName audioPropertyName, BlackboardPropertyName valuePropertyName)
		{
			m_audioPropertyName = audioPropertyName;
			m_valuePropertyName = valuePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_audioPropertyName, out AudioSource audio) & audio != null)
			{
				blackboard.SetStructValue(m_valuePropertyName, audio.isPlaying);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
