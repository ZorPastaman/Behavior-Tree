// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// <para>
	/// This <see cref="Decorator"/> ticks its child
	/// with a variable cooldown duration after a tick with a <see cref="Status.Success"/> result.
	/// </para>
	/// <para>
	/// If this <see cref="Decorator"/> isn't in a cooldown, it ticks with a result of its child.
	/// If its child ticks with a <see cref="Status.Success"/> result, this <see cref="Decorator"/> starts a cooldown.
	/// In the cooldown state this <see cref="Decorator"/> ticks with a <see cref="Status.Failure"/> result.
	/// </para>
	/// <para>
	/// This <see cref="Decorator"/> uses <see cref="Time.frameCount"/> as a frame counter.
	/// </para>
	/// <para>
	/// This behavior uses a <see cref="Blackboard"/> property of type <see cref="int"/> as a cooldown duration.
	/// </para>
	/// <para>
	/// The property name of the cooldown duration of type <see cref="int"/> is set in the setup method.
	/// </para>
	/// </summary>
	public sealed class CooldownOfFramesVariable : Decorator, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_durationPropertyName;

		[BehaviorInfo] private int m_lastChildTickFrame;
		private bool m_isLastTickSuccess;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName durationPropertyName)
		{
			SetupInternal(durationPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string durationPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(durationPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName durationPropertyName)
		{
			m_durationPropertyName = durationPropertyName;
		}

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_durationPropertyName, out int duration))
			{
				return Status.Error;
			}

			int frame = Time.frameCount;

			if (m_isLastTickSuccess & (frame - m_lastChildTickFrame < duration))
			{
				return Status.Failure;
			}

			Status childStatus = child.Tick();
			m_lastChildTickFrame = frame;
			m_isLastTickSuccess = childStatus == Status.Success;

			return childStatus;
		}
	}
}
