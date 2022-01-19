// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.BehaviorTree.Debugging;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core
{
	/// <summary>
	/// Base class for behavior tree behaviors.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Don't derive it. You need to derive <see cref="Zor.BehaviorTree.Core.Composites.Composite"/>,
	/// <see cref="Zor.BehaviorTree.Core.Decorators.Decorator"/>,
	/// <see cref="Zor.BehaviorTree.Core.Leaves.Actions.Action"/>,
	/// <see cref="Zor.BehaviorTree.Core.Leaves.Conditions.Condition"/> or
	/// <see cref="Zor.BehaviorTree.Core.Leaves.Leaf"/>.
	/// </para>
	/// <para>
	/// In setup methods you always don't have children. They're set right after a setup.
	/// </para>
	/// </remarks>
	public abstract class Behavior
	{
		/// <summary>
		/// Behavior tree blackboard.
		/// </summary>
		private Blackboard m_blackboard;
		/// <summary>
		/// Current status. It's updated after each tick.
		/// </summary>
		private Status m_status = Status.Idle;

		/// <summary>
		/// Current status. It's updated after each tick.
		/// </summary>
		public Status status
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_status;
		}

		/// <summary>
		/// Behavior tree blackboard.
		/// </summary>
		protected Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

		/// <summary>
		/// Ticks a behavior.
		/// </summary>
		/// <returns>New status of the behavior.</returns>
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

#if DEBUG
			if (m_status != Status.Success & m_status != Status.Running & m_status != Status.Failure & m_status != Status.Error)
			{
				BehaviorTreeDebug.LogError($"Behavior of type {GetType()} returned {m_status} as a result of Execute. But only {Status.Success}, {Status.Running}, {Status.Failure} and {Status.Error} are acceptable");
			}
#endif

			Profiler.EndSample();

			return m_status;
		}

		/// <summary>
		/// Aborts a behavior if it's in <see cref="Status.Running"/> state.
		/// </summary>
		/// <returns>
		/// <para>
		/// New state (<see cref="Status.Abort"/>) if the behavior was in <see cref="Status.Running"/> state
		/// before the call.
		/// </para>
		/// <para>Current state if the behavior wasn't in <see cref="Status.Running"/> state before the call.</para>
		/// </returns>
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

		/// <summary>
		/// Initializes a behavior. It's called once before a first tick.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void Initialize()
		{
			Profiler.BeginSample(GetType().FullName);
			OnInitialize();
			Profiler.EndSample();
		}

		/// <summary>
		/// Disposes a behavior. It's called once before a behavior tree is destroyed.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void Dispose()
		{
			Profiler.BeginSample(GetType().FullName);
			OnDispose();
			Profiler.EndSample();
		}

		/// <summary>
		/// Sets <paramref name="blackboardToSet"/> as a behavior blackboard.
		/// It's called once right after a creation of a behavior tree.
		/// </summary>
		/// <param name="blackboardToSet">Blackboard to set.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void SetBlackboard([NotNull] Blackboard blackboardToSet)
		{
			m_blackboard = blackboardToSet;
		}

		/// <summary>
		/// The method is called once before a first tick.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void OnInitialize() {}

		/// <summary>
		/// The method is called each tick before <see cref="Execute"/>
		/// if <see cref="status"/> is not <see cref="Status.Running"/>.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void Begin() {}

		/// <summary>
		/// The method is called each tick. Its returned value is set to <see cref="status"/> right after the call.
		/// </summary>
		/// <returns>Result of the execution.</returns>
		/// <remarks>
		/// The method must return one of these values: <see cref="Status.Success"/>, <see cref="Status.Running"/>,
		/// <see cref="Status.Failure"/> or <see cref="Status.Error"/>.
		/// </remarks>
		protected abstract Status Execute();

		/// <summary>
		/// The method is called each tick after <see cref="Execute"/>
		/// if <see cref="status"/> after <see cref="Execute"/> is not <see cref="Status.Running"/>.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void End() {}

		/// <summary>
		/// The method is called when <see cref="Abort"/> is called and
		/// current <see cref="status"/> is <see cref="Status.Running"/>.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void OnAbort() {}

		/// <summary>
		/// The method is called once before a behavior tree is destroyed.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void OnDispose() {}

		/// <summary>
		/// The method is called before <see cref="OnAbort"/>. It's used to call <see cref="OnAbort"/>
		/// in from child to parent order.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected private virtual void OnAbortInternal() {}

		/// <summary>
		/// Calls an appropriate setup method of <paramref name="behavior"/>.
		/// </summary>
		/// <param name="behavior"><see cref="Behavior"/> with the setup method.</param>
		/// <param name="parameters">Setup method arguments.</param>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="behavior"/> doesn't have an appropriate setup method.
		/// </exception>
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
					throw new ArgumentException($"Failed to setup a behavior of type {behaviorType}. Too many parameters are passed. It supports up to 8 parameters.");
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

			throw new ArgumentException($"Failed to setup a behavior of type {behaviorType}. It doesn't have an appropriate setup method for passed arguments.");
		}
	}
}
