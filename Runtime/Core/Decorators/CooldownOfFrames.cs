// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class CooldownOfFrames : Decorator, ISetupable<int>
	{
		[BehaviorInfo] private int m_duration;

		[BehaviorInfo] private int m_lastChildTickFrame;
		private bool m_isLastTickSuccess;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(int duration)
		{
			m_duration = duration;
		}

		protected override Status Execute()
		{
			int frame = Time.frameCount;

			if (m_isLastTickSuccess & (frame - m_lastChildTickFrame < m_duration))
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
