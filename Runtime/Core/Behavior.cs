// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core
{
	public abstract class Behavior
	{
		private Blackboard m_blackboard;
		private Status m_status = Status.Idle;

		public Status status
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_status;
		}

		protected Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

		public Status Tick()
		{
			if (m_status != Status.Running)
			{
				Begin();
			}

			m_status = Execute();

			if (m_status != Status.Running)
			{
				End();
			}

			return m_status;
		}

		public Status Abort()
		{
			if (m_status == Status.Running)
			{
				OnAbortInternal();
				OnAbort();
				m_status = Status.Abort;
			}

			return m_status;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void Initialize()
		{
			OnInitialize();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void Dispose()
		{
			OnDispose();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void SetBlackboard([NotNull] Blackboard blackboardToSet)
		{
			m_blackboard = blackboardToSet;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void OnInitialize() {}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void Begin() {}

		protected abstract Status Execute();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void End() {}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void OnAbort() {}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void OnDispose() {}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected private virtual void OnAbortInternal() {}

		protected private static void CreateSetup([NotNull] Behavior behavior, [NotNull] object[] parameters)
		{
			int parametersCount = parameters.Length;
			var parameterTypes = new Type[parametersCount];

			for (int i = 0; i < parametersCount; ++i)
			{
				parameterTypes[i] = parameters[i].GetType();
			}

			Type baseSetupableType;

			switch (parametersCount)
			{
				case 1:
					baseSetupableType = typeof(ISetupable<>);
					break;
				case 2:
					baseSetupableType = typeof(ISetupable<,>);
					break;
				case 3:
					baseSetupableType = typeof(ISetupable<,,>);
					break;
				case 4:
					baseSetupableType = typeof(ISetupable<,,,>);
					break;
				case 5:
					baseSetupableType = typeof(ISetupable<,,,,>);
					break;
				case 6:
					baseSetupableType = typeof(ISetupable<,,,,,>);
					break;
				case 7:
					baseSetupableType = typeof(ISetupable<,,,,,,>);
					break;
				default:
					return;
			}

			Type setupableType = baseSetupableType.MakeGenericType(parameterTypes);
			setupableType.InvokeMember("Setup", BindingFlags.InvokeMethod, null, behavior, parameters);
		}
	}
}
