// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Gets a <see cref="Component.transform"/> and sets it into the <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	///  <list type="bullet">
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
	/// 		<description>Property name of a component of type <see cref="Component"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Transform"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class GetComponentTransform : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_componentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName componentPropertyName, BlackboardPropertyName transformPropertyName)
		{
			SetupInternal(componentPropertyName, transformPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string componentPropertyName, string transformPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(componentPropertyName),
				new BlackboardPropertyName(transformPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName componentPropertyName,
			BlackboardPropertyName transformPropertyName)
		{
			m_componentPropertyName = componentPropertyName;
			m_transformPropertyName = transformPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_componentPropertyName, out Component component) & component != null)
			{
				blackboard.SetClassValue(m_transformPropertyName, component.transform);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
