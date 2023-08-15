﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a class property in the <see cref="Blackboard"/> equals another property.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the properties are equal.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the properties aren't equal.</description>
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
	public sealed class IsClassValueEqualVariable<T> : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
		where T : class
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_secondPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName firstPropertyName, BlackboardPropertyName secondPropertyName)
		{
			SetupInternal(firstPropertyName, secondPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string firstPropertyName, string secondPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(firstPropertyName),
				new BlackboardPropertyName(secondPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName firstPropertyName, BlackboardPropertyName secondPropertyName)
		{
			m_firstPropertyName = firstPropertyName;
			m_secondPropertyName = secondPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetClassValue(m_firstPropertyName, out T firstValue) &
				blackboard.TryGetClassValue(m_secondPropertyName, out T secondValue);
			return StateToStatusHelper.ConditionToStatus(Equals(firstValue, secondValue), hasValues);
		}
	}
}
