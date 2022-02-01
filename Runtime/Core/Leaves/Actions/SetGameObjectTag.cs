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
	/// 		<description>Tag of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetGameObjectTag : Action,
		ISetupable<BlackboardPropertyName, string>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;
		[BehaviorInfo] private string m_tag;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string>.Setup(BlackboardPropertyName gameObjectPropertyName, string tag)
		{
			SetupInternal(gameObjectPropertyName, tag);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string gameObjectPropertyName, string tag)
		{
			SetupInternal(new BlackboardPropertyName(gameObjectPropertyName), tag);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName gameObjectPropertyName, string tag)
		{
			m_gameObjectPropertyName = gameObjectPropertyName;
			m_tag = tag;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_gameObjectPropertyName, out GameObject gameObject) & gameObject != null)
			{
				gameObject.tag = m_tag;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
