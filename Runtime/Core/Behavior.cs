// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Profiling;
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
			Profiler.BeginSample(GetType().FullName);

			if (m_status != Status.Running)
			{
				Profiler.BeginSample("Begin");
				Begin();
				Profiler.EndSample();
			}

			Profiler.BeginSample("Execute");
			m_status = Execute();
			Profiler.EndSample();

			if (m_status != Status.Running)
			{
				Profiler.BeginSample("End");
				End();
				Profiler.EndSample();
			}

			Profiler.EndSample();

			return m_status;
		}

		public Status Abort()
		{
			Profiler.BeginSample(GetType().FullName);

			if (m_status == Status.Running)
			{
				OnAbortInternal();

				Profiler.BeginSample("OnAbort");
				OnAbort();
				Profiler.EndSample();

				m_status = Status.Abort;
			}

			Profiler.EndSample();

			return m_status;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void Initialize()
		{
			Profiler.BeginSample(GetType().FullName);
			OnInitialize();
			Profiler.EndSample();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void Dispose()
		{
			Profiler.BeginSample(GetType().FullName);
			OnDispose();
			Profiler.EndSample();
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

		protected private static void CreateSetup([NotNull] Behavior behavior,
			[NotNull, ItemCanBeNull] object[] parameters)
		{
			Type behaviorType = behavior.GetType();
			Type[] interfaceTypes = behaviorType.GetInterfaces();

			int parametersCount = parameters.Length;
			var parameterTypes = new Type[parametersCount];

			for (int i = 0; i < parametersCount; ++i)
			{
				object parameter = parameters[i];
				parameterTypes[i] = parameter?.GetType();
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
				case 8:
					baseSetupableType = typeof(ISetupable<,,,,,,,>);
					break;
				default:
					throw new Exception();
			}

			for (int i = 0, iCount = interfaceTypes.Length; i < iCount; ++i)
			{
				Type interfaceType = interfaceTypes[i];

				// TODO Support derivation
				if (!interfaceType.IsGenericType || interfaceType.GetGenericTypeDefinition() != baseSetupableType)
				{
					continue;
				}

				Type[] interfaceParameters = interfaceType.GetGenericArguments();
				bool goodInterface = true;

				for (int j = 0, jCount = interfaceParameters.Length; j < jCount & goodInterface; ++j)
				{
					goodInterface = parameterTypes[j] == null
						? interfaceParameters[j].IsClass
						: interfaceParameters[j].IsAssignableFrom(parameterTypes[j]);
				}

				if (!goodInterface)
				{
					continue;
				}

				Profiler.BeginSample("Setup");
				interfaceType.InvokeMember("Setup", BindingFlags.InvokeMethod, null, behavior, parameters);
				Profiler.EndSample();

				return;
			}

			throw new Exception();
		}
	}
}
