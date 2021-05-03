﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class LimitOfFrames : Decorator, ISetupable<int>
	{
		[BehaviorInfo] private int m_duration;

		[BehaviorInfo] private int m_beginFrame;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(int duration)
		{
			m_duration = duration;
		}

		protected override void Begin()
		{
			base.Begin();
			m_beginFrame = Time.frameCount;
		}

		protected override unsafe Status Execute()
		{
			Status childStatus = child.Tick();
			Status* results = stackalloc[] {childStatus, Status.Failure};
			bool isTimeOver = childStatus == Status.Running & (Time.frameCount - m_beginFrame >= m_duration);
			byte index = *(byte*)&isTimeOver;

			return results[index];
		}
	}
}
