// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class TransformTranslate : Action,
		ISetupable<BlackboardPropertyName, Vector3>, ISetupable<string, Vector3>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private Vector3 m_translation;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, Vector3>.Setup(BlackboardPropertyName transformPropertyName,
			Vector3 translation)
		{
			SetupInternal(transformPropertyName, translation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, Vector3>.Setup(string transformPropertyName, Vector3 translation)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName), translation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName, Vector3 translation)
		{
			m_transformPropertyName = transformPropertyName;
			m_translation = translation;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null)
			{
				transform.Translate(m_translation);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
