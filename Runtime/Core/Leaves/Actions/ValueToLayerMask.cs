// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Converts <see cref="int"/> into <see cref="LayerMask"/> and sets a result into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a source of type <see cref="int"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class ValueToLayerMask : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName valuePropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			SetupInternal(valuePropertyName, layerMaskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string valuePropertyName, string layerMaskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(valuePropertyName),
				new BlackboardPropertyName(layerMaskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName valuePropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			m_valuePropertyName = valuePropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_valuePropertyName, out int value))
			{
				LayerMask layerMask = value;
				blackboard.SetStructValue(m_layerMaskPropertyName, layerMask);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
