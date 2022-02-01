// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="GameObject"/>'s tag using <see cref="GameObject.tag"/>.
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
	/// 		<description>Property name of a game object of type <see cref="GameObject"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a tag of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetGameObjectTagVariable : Action, 
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_tagPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName gameObjectPropertyName, BlackboardPropertyName tagPropertyName)
		{
			SetupInternal(gameObjectPropertyName, tagPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string gameObjectPropertyName, string tagPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(gameObjectPropertyName),
				new BlackboardPropertyName(tagPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName gameObjectPropertyName,
			BlackboardPropertyName tagPropertyName)
		{
			m_gameObjectPropertyName = gameObjectPropertyName;
			m_tagPropertyName = tagPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_gameObjectPropertyName, out GameObject gameObject) & gameObject != null &
				blackboard.TryGetClassValue(m_tagPropertyName, out string tag))
			{
				gameObject.tag = tag;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
