// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a cursor lock state using <see cref="Cursor.lockState"/>.
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
	/// 		<description>Property name of a lock state of type <see cref="CursorLockMode"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetCursorLockStateVariable : Action, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_lockStatePropertyName;

		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName lockStatePropertyName)
		{
			SetupInternal(lockStatePropertyName);
		}

		void ISetupable<string>.Setup(string lockStatePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(lockStatePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName lockStatePropertyName)
		{
			m_lockStatePropertyName = lockStatePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_lockStatePropertyName, out CursorLockMode lockState))
			{
				Cursor.lockState = lockState;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
