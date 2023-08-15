// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Converts a <see cref="Color"/> into a <see cref="Vector4"/>.
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
	/// 		<description>Property name of a color of type <see cref="Color"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a vector of type <see cref="Vector4"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
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
