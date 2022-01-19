// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Plays an <see cref="AudioClip"/> on an <see cref="AudioSource"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if there's all the data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Error"/> </term>
	/// 		<description>if there's no data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Audio clip of type <see cref="AudioClip"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of an audio source of type <see cref="AudioSource"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class PlayOneShotAudioSource : Action,
		ISetupable<AudioClip, BlackboardPropertyName>, ISetupable<AudioClip, string>
	{
		[BehaviorInfo] private AudioClip m_clip;
		[BehaviorInfo] private BlackboardPropertyName m_audioPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<AudioClip, BlackboardPropertyName>.Setup(AudioClip clip, BlackboardPropertyName audioPropertyName)
		{
			SetupInternal(clip, audioPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<AudioClip, string>.Setup(AudioClip clip, string audioPropertyName)
		{
			SetupInternal(clip, new BlackboardPropertyName(audioPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(AudioClip clip, BlackboardPropertyName audioPropertyName)
		{
			m_clip = clip;
			m_audioPropertyName = audioPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_audioPropertyName, out AudioSource audio) & audio != null)
			{
				audio.PlayOneShot(m_clip);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
