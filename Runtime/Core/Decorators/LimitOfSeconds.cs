﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class LimitOfSeconds : Decorator, ISetupable<float>
	{
		[BehaviorInfo] private float m_duration;

		[BehaviorInfo] private float m_beginTime;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(float duration)
		{
			m_duration = duration;
		}

		protected override void Begin()
		{
			base.Begin();
			m_beginTime = Time.time;
		}

		protected override unsafe Status Execute()
		{
			Status childStatus = child.Tick();
			Status* results = stackalloc[] {childStatus, Status.Failure};
			bool isTimeOver = childStatus == Status.Running & (Time.time - m_beginTime >= m_duration);
			byte index = *(byte*)&isTimeOver;

			return results[index];
		}
	}
}