// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if the <see cref="Blackboard"/> has a struct property of a specified type and a name.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the <see cref="Blackboard"/> has such a property.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the <see cref="Blackboard"/> doesn't have such a property.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Property name of the struct of type <typeparamref name="T"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <typeparam name="T">Struct type.</typeparam>
	public sealed class HasStructValue<T> : Condition, ISetupable<BlackboardPropertyName>, ISetupable<string>
		where T : struct
	{
		[BehaviorInfo] private BlackboardPropertyName m_propertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName propertyName)
		{
			SetupInternal(propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string propertyName)
		{
			SetupInternal(new BlackboardPropertyName(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		protected override Status Execute()
		{
			return StateToStatusHelper.ConditionToStatus(blackboard.ContainsStructValue<T>(m_propertyName));
		}
	}
}
