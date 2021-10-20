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
			return (Status)((int)Status.Running >> *(byte*)&isFinished);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FinishedToStatus(bool isFinished, bool isValid)
		{
			return ConditionToStatus(isValid, Status.Error, FinishedToStatus(isFinished));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FailedToStatus(bool isFailed)
		{
			return (Status)((int)Status.Running << *(byte*)&isFailed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FailedToStatus(bool isFailed, bool isValid)
		{
			return ConditionToStatus(isValid, Status.Error, FailedToStatus(isFailed));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FinishedFailedToStatus(bool isFinished, bool isFailed)
		{
			Status falseStatus = FinishedFailedToStatusSimple(isFinished, isFailed);
			return ConditionToStatus(isFinished & isFailed, falseStatus, Status.Error);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FinishedFailedToStatus(bool isFinished, bool isFailed, bool isValid)
		{
			Status trueStatus = FinishedFailedToStatusSimple(isFinished, isFailed);
			return ConditionToStatus(!(isFinished & isFailed) & isValid, Status.Error, trueStatus);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status ConditionToStatus(bool condition)
		{
			return (Status)((int)Status.Failure >> (*(byte*)&condition << 1));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status ConditionToStatus(bool condition, bool isValid)
		{
			return ConditionToStatus(isValid, Status.Error, ConditionToStatus(condition));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status ConditionToStatus(bool condition, Status falseStatus, Status trueStatus)
		{
			int conditionMask = -(*(byte*)&condition);
			return (Status)(((int)trueStatus & conditionMask) | ((int)falseStatus & ~conditionMask));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		private static unsafe Status FinishedFailedToStatusSimple(bool isFinished, bool isFailed)
		{
			return (Status)((int)Status.Running >> *(byte*)&isFinished << *(byte*)&isFailed);
		}
	}
}
