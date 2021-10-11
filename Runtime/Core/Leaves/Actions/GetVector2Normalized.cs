// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetVector2Normalized : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_normalizedPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName vectorPropertyName,
			BlackboardPropertyName normalizedPropertyName)
		{
			SetupInternal(vectorPropertyName, normalizedPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string vectorPropertyName, string normalizedPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName),
				new BlackboardPropertyName(normalizedPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName,
			BlackboardPropertyName normalizedPropertyName)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_normalizedPropertyName = normalizedPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_vectorPropertyName, out Vector2 vector))
			{
				blackboard.SetStructValue(m_normalizedPropertyName, vector.normalized);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
