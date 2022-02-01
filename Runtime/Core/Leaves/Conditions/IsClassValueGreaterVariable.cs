// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a class property in the <see cref="Blackboard"/> is greater than another property.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the first property is greater than the second property.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the first property isn't greater than the second property.</description>
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
	/// 		<description>Property name of a first class property of type <typeparamref name="T"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second class property of type <typeparamref name="T"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <typeparam name="T">Class type.</typeparam>
	public sealed class IsClassValueGreaterVariable<T> : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
		where T : class, IComparable<T>
	{
		[BehaviorInfo] private BlackboardPropertyName m_leftPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rightPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName leftPropertyName, BlackboardPropertyName rightPropertyName)
		{
			SetupInternal(leftPropertyName, rightPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string leftPropertyName, string rightPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(leftPropertyName), new BlackboardPropertyName(rightPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName leftPropertyName, BlackboardPropertyName rightPropertyName)
		{
			m_leftPropertyName = leftPropertyName;
			m_rightPropertyName = rightPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetClassValue(m_leftPropertyName, out T leftValue) &
				blackboard.TryGetClassValue(m_rightPropertyName, out T rightValue);
			bool isGreater = rightValue == null ? leftValue != null : rightValue.CompareTo(leftValue) < 0;
			return StateToStatusHelper.ConditionToStatus(isGreater, hasValues);
		}
	}
}
