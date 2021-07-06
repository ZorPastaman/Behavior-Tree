// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class ColorToVector4 : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;

		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName colorPropertyName,
			BlackboardPropertyName vectorPropertyName)
		{
			SetupInternal(colorPropertyName, vectorPropertyName);
		}

		void ISetupable<string, string>.Setup(string colorPropertyName, string vectorPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colorPropertyName),
				new BlackboardPropertyName(vectorPropertyName));
		}

		private void SetupInternal(BlackboardPropertyName colorPropertyName, BlackboardPropertyName vectorPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_vectorPropertyName = vectorPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_colorPropertyName, out Color color))
			{
				var vector = (Vector4)color;
				blackboard.SetStructValue(m_vectorPropertyName, vector);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
