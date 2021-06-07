// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Core
{
	public static class StateToStatusHelper
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FinishedToStatus(bool isFinished)
		{
			Status* results = stackalloc Status[] {Status.Running, Status.Success};
			byte index = *(byte*)&isFinished;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FinishedToStatus(bool isFinished, bool isValid)
		{
			Status* results = stackalloc Status[] {Status.Error, Status.Running, Status.Success};
			int index = *(byte*)&isValid << *(byte*)&isFinished;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FailedToStatus(bool isFailed)
		{
			Status* results = stackalloc Status[] {Status.Running, Status.Failure};
			byte index = *(byte*)&isFailed;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FailedToStatus(bool isFailed, bool isValid)
		{
			Status* results = stackalloc Status[] {Status.Error, Status.Running, Status.Failure};
			int index = *(byte*)&isValid << *(byte*)&isFailed;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FinishedFailedToStatus(bool isFinished, bool isFailed)
		{
			Status* results = stackalloc Status[] {Status.Running, Status.Failure, Status.Success, Status.Error};
			int index = (*(byte*)&isFinished << 1) | *(byte*)&isFailed;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FinishedFailedToStatus(bool isFinished, bool isFailed, bool isValid)
		{
			isFinished |= !isValid;
			isFailed |= !isValid;
			Status* results = stackalloc Status[] {Status.Running, Status.Failure, Status.Success, Status.Error};
			int index = (*(byte*)&isFinished << 1) | *(byte*)&isFailed;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status ConditionToStatus(bool condition)
		{
			Status* results = stackalloc Status[] {Status.Failure, Status.Success};
			byte index = *(byte*)&condition;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status ConditionToStatus(bool condition, bool isValid)
		{
			Status* results = stackalloc Status[] {Status.Error, Status.Failure, Status.Success};
			int index = *(byte*)&isValid << *(byte*)&condition;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status ConditionToStatus(bool condition, Status falseStatus, Status trueStatus)
		{
			Status* results = stackalloc Status[] {falseStatus, trueStatus};
			byte index = *(byte*)&condition;

			return results[index];
		}
	}
}
