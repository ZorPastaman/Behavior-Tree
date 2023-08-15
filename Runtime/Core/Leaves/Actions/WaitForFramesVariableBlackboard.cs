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
	/// 		<description>Property name of a duration in frames of type <see cref="int"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class WaitForFramesVariableBlackboard : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_framePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_durationPropertyName;

		[BehaviorInfo] private int m_beginFrame;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName framePropertyName,
			BlackboardPropertyName durationPropertyName)
		{
			SetupInternal(framePropertyName, durationPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string framePropertyName, string durationPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(framePropertyName),
				new BlackboardPropertyName(durationPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName framePropertyName,
			BlackboardPropertyName durationPropertyName)
		{
			m_framePropertyName = framePropertyName;
			m_durationPropertyName = durationPropertyName;
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
			bool hasValues = blackboard.TryGetStructValue(m_framePropertyName, out int frame) &
				blackboard.TryGetStructValue(m_durationPropertyName, out int duration);
			return StateToStatusHelper.FinishedToStatus(frame - m_beginFrame >= duration, hasValues);
		}
	}
}
