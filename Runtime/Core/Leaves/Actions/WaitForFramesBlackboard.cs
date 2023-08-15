// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Waits for fixed duration returning <see cref="Status.Running"/> in its tick.
	/// When the duration is expired, returns <see cref="Status.Success"/>.
	/// This wait uses a <see cref="Blackboard"/> property of type <see cref="int"/> as a frame counter.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>when the duration is expired.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Running"/> </term>
	/// 		<description>when the duration isn't expired.</description>
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
	/// 		<description>Property name of a frame counter of type <see cref="int"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Duration in frames of type <see cref="int"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class WaitForFramesBlackboard : Action,
		ISetupable<BlackboardPropertyName, int>, ISetupable<string, int>
	{
		[BehaviorInfo] private BlackboardPropertyName m_framePropertyName;
		[BehaviorInfo] private int m_duration;

		[BehaviorInfo] private int m_beginFrame;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int>.Setup(BlackboardPropertyName framePropertyName, int duration)
		{
			SetupInternal(framePropertyName, duration);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int>.Setup(string framePropertyName, int duration)
		{
			SetupInternal(new BlackboardPropertyName(framePropertyName), duration);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName framePropertyName, int duration)
		{
			m_framePropertyName = framePropertyName;
			m_duration = duration;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			blackboard.TryGetStructValue(m_framePropertyName, out m_beginFrame);
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasFrame = blackboard.TryGetStructValue(m_framePropertyName, out int frame);
			return StateToStatusHelper.FinishedToStatus(frame - m_beginFrame >= m_duration, hasFrame);
		}
	}
}
