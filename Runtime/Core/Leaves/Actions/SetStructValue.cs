// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a struct into the <see cref="Blackboard"/>.
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
	/// 		<description>Struct value of type <typeparamref name="T"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for the struct.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <typeparam name="T">Struct type.</typeparam>
	public sealed class SetStructValue<T> : Action, ISetupable<T, BlackboardPropertyName>, ISetupable<T, string>
		where T : struct
	{
		[BehaviorInfo] private T m_value;
		[BehaviorInfo] private BlackboardPropertyName m_propertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<T, BlackboardPropertyName>.Setup(T value, BlackboardPropertyName propertyName)
		{
			SetupInternal(value, propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<T, string>.Setup(T value, string propertyName)
		{
			SetupInternal(value, new BlackboardPropertyName(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_propertyName, m_value);
			return Status.Success;
		}
	}
}
