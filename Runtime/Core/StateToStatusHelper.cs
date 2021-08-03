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
			const int running = (int)Status.Running;
			const int runningSuccessDiff = (int)Status.Success - (int)Status.Running;

			return (Status)(running + *(byte*)&isFinished * runningSuccessDiff);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FinishedToStatus(bool isFinished, bool isValid)
		{
			return ConditionToStatus(isValid, Status.Error, FinishedToStatus(isFinished));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FailedToStatus(bool isFailed)
		{
			const int running = (int)Status.Running;
			const int runningFailureDiff = Status.Failure - Status.Running;

			return (Status)(running + *(byte*)&isFailed * runningFailureDiff);
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
			const int failure = (int)Status.Failure;
			const int failureSuccessDiff = (int)Status.Success - (int)Status.Failure;

			return (Status)(failure + *(byte*)&condition * failureSuccessDiff);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status ConditionToStatus(bool condition, bool isValid)
		{
			return ConditionToStatus(isValid, Status.Error, ConditionToStatus(condition));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status ConditionToStatus(bool condition, Status falseStatus, Status trueStatus)
		{
			int statusDiff = trueStatus - falseStatus;
			return (Status)((int)falseStatus + *(byte*)&condition * statusDiff);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		private static unsafe Status FinishedFailedToStatusSimple(bool isFinished, bool isFailed)
		{
			const int running = (int)Status.Running;
			const int runningFailureDiff = Status.Failure - Status.Running;
			const int runningSuccessDiff = (int)Status.Success - (int)Status.Running;

			return (Status)(running + *(byte*)&isFinished * runningSuccessDiff +
				*(byte*)&isFailed * runningFailureDiff);
		}
	}
}
