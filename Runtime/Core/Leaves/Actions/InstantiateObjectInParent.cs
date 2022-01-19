// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Instantiates an <see cref="Object"/> in a parent and sets it into the <see cref="Blackboard"/>.
	/// This <see cref="Action"/> uses <see cref="Object.Instantiate(Object, Transform)"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
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
	/// 		<description>Property name of an object of type <see cref="Object"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a parent of type <see cref="Transform"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Object"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class InstantiateObjectInParent : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_objectPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_parentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName objectPropertyName, BlackboardPropertyName parentPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(objectPropertyName, parentPropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string objectPropertyName, string parentPropertyName,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(objectPropertyName),
				new BlackboardPropertyName(parentPropertyName), new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName objectPropertyName, BlackboardPropertyName parentPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			m_objectPropertyName = objectPropertyName;
			m_parentPropertyName = parentPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_objectPropertyName, out Object @object) & @object != null &
				blackboard.TryGetClassValue(m_parentPropertyName, out Transform parent) & parent != null)
			{
				blackboard.SetClassValue(m_resultPropertyName, Object.Instantiate(@object, parent));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
