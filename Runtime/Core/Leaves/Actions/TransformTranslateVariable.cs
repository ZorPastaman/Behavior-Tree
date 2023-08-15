// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Moves a <see cref="Transform"/> with <see cref="Transform.Translate(Vector3)"/>.
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
	/// 		<description>Property name of a transform of type <see cref="Transform"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a translation of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class TransformTranslateVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_translationPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName transformPropertyName, BlackboardPropertyName translationPropertyName)
		{
			SetupInternal(transformPropertyName, translationPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string transformPropertyName, string translationPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName),
				new BlackboardPropertyName(translationPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName,
			BlackboardPropertyName translationPropertyName)
		{
			m_transformPropertyName = transformPropertyName;
			m_translationPropertyName = translationPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null &
				blackboard.TryGetStructValue(m_translationPropertyName, out Vector3 translation))
			{
				transform.Translate(translation);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
