// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="Vector2"/> into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of x of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of y type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Vector2"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class SetVector2Variable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_xPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_yPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName xPropertyName, BlackboardPropertyName yPropertyName,
			BlackboardPropertyName vectorPropertyName)
		{
			SetupInternal(xPropertyName, yPropertyName, vectorPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string xPropertyName, string yPropertyName,
			string vectorPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(xPropertyName), new BlackboardPropertyName(yPropertyName),
				new BlackboardPropertyName(vectorPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName xPropertyName, BlackboardPropertyName yPropertyName,
			BlackboardPropertyName vectorPropertyName)
		{
			m_xPropertyName = xPropertyName;
			m_yPropertyName = yPropertyName;
			m_vectorPropertyName = vectorPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_xPropertyName, out float x) &
				blackboard.TryGetStructValue(m_yPropertyName, out float y))
			{
				blackboard.SetStructValue(m_vectorPropertyName, new Vector2(x, y));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
