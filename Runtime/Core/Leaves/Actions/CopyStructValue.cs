// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Copies a struct value from one property in the <see cref="Blackboard"/> to another.
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
	/// 		<description>Property name of source of type <typeparamref name="T"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of destination of type <typeparamref name="T"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	/// <typeparam name="T">Struct type.</typeparam>
	public sealed class CopyStructValue<T> : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
		where T : struct
	{
		[BehaviorInfo] private BlackboardPropertyName m_sourcePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_destinationPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName sourcePropertyName, BlackboardPropertyName destinationPropertyName)
		{
			SetupInternal(sourcePropertyName, destinationPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string sourcePropertyName, string destinationPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(sourcePropertyName),
				new BlackboardPropertyName(destinationPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName sourcePropertyName,
			BlackboardPropertyName destinationPropertyName)
		{
			m_sourcePropertyName = sourcePropertyName;
			m_destinationPropertyName = destinationPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_sourcePropertyName, out T value))
			{
				blackboard.SetStructValue(m_destinationPropertyName, value);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
