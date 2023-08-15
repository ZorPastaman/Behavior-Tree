// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Core
{
	/// <summary>
	/// Helper that allows you to convert fast <see langword="bool"/> values into <see cref="Status"/> values.
	/// </summary>
	public static class StateToStatusHelper
	{
		/// <summary>
		/// Converts <paramref name="isFinished"/> into a <see cref="Status"/> value.
		/// </summary>
		/// <param name="isFinished">Value input.</param>
		/// <returns>
		/// <para><see cref="Status.Success"/> if <paramref name="isFinished"/> is <see langword="true"/>.</para>
		/// <para><see cref="Status.Running"/> if <paramref name="isFinished"/> is <see langword="false"/>.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FinishedToStatus(bool isFinished)
		{
			return (Status)((int)Status.Running >> *(byte*)&isFinished);
		}

		/// <summary>
		/// Converts <paramref name="isFinished"/> into a <see cref="Status"/> value with a validity check.
		/// </summary>
		/// <param name="isFinished">Value input.</param>
		/// <param name="isValid">Validity input.</param>
		/// <returns>
		/// <para>
		/// <see cref="Status.Success"/> if <paramref name="isFinished"/> is <see langword="true"/>
		/// and <paramref name="isValid"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Running"/> if <paramref name="isFinished"/> is <see langword="false"/>
		/// and <paramref name="isValid"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Error"/> if <paramref name="isValid"/> is <see langword="false"/>.
		/// <paramref name="isFinished"/> can be any in this case.
		/// </para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FinishedToStatus(bool isFinished, bool isValid)
		{
			return ConditionToStatus(isValid, Status.Error, FinishedToStatus(isFinished));
		}

		/// <summary>
		/// Converts <paramref name="isFailed"/> into a <see cref="Status"/> value.
		/// </summary>
		/// <param name="isFailed">Value input.</param>
		/// <returns>
		/// <para><see cref="Status.Failure"/> if <paramref name="isFailed"/> is <see langword="true"/>.</para>
		/// <para><see cref="Status.Running"/> if <paramref name="isFailed"/> is <see langword="false"/>.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status FailedToStatus(bool isFailed)
		{
			return (Status)((int)Status.Running << *(byte*)&isFailed);
		}

		/// <summary>
		/// Converts <paramref name="isFailed"/> into a <see cref="Status"/> value with a validity check.
		/// </summary>
		/// <param name="isFailed">Value input.</param>
		/// <param name="isValid">Validity input.</param>
		/// <returns>
		/// <para>
		/// <see cref="Status.Failure"/> if <paramref name="isFailed"/> is <see langword="true"/>
		/// and <paramref name="isValid"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Running"/> if <paramref name="isFailed"/> is <see langword="false"/>
		/// and <paramref name="isValid"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Error"/> if <paramref name="isValid"/> is <see langword="false"/>.
		/// <paramref name="isFailed"/> can be any in this case.
		/// </para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FailedToStatus(bool isFailed, bool isValid)
		{
			return ConditionToStatus(isValid, Status.Error, FailedToStatus(isFailed));
		}

		/// <summary>
		/// Converts <paramref name="isFinished"/> and <paramref name="isFailed"/> into a <see cref="Status"/> value.
		/// </summary>
		/// <param name="isFinished">Finished input.</param>
		/// <param name="isFailed">Failed input.</param>
		/// <returns>
		/// <para>
		/// <see cref="Status.Success"/> if <paramref name="isFinished"/> is <see langword="true"/>
		/// and <paramref name="isFailed"/> is <see langword="false"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Failure"/> if <paramref name="isFinished"/> is <see langword="false"/>
		/// and <paramref name="isFailed"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Running"/> if both <paramref name="isFinished"/> and <paramref name="isFailed"/>
		/// are <see langword="false"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Error"/> if both <paramref name="isFinished"/> and <paramref name="isFailed"/>
		/// are <see langword="true"/>.
		/// </para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FinishedFailedToStatus(bool isFinished, bool isFailed)
		{
			Status falseStatus = FinishedFailedToStatusSimple(isFinished, isFailed);
			return ConditionToStatus(isFinished & isFailed, falseStatus, Status.Error);
		}

		/// <summary>
		/// Converts <paramref name="isFinished"/> and <paramref name="isFailed"/> into a <see cref="Status"/> value
		/// with a validity check.
		/// </summary>
		/// <param name="isFinished">Finished input.</param>
		/// <param name="isFailed">Failed input.</param>
		/// <param name="isValid">Validity input.</param>
		/// <returns>
		/// <para>
		/// <see cref="Status.Success"/> if <paramref name="isFinished"/> is <see langword="true"/>
		/// and <paramref name="isFailed"/> is <see langword="false"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Failure"/> if <paramref name="isFinished"/> is <see langword="false"/>
		/// and <paramref name="isFailed"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Running"/> if both <paramref name="isFinished"/> and <paramref name="isFailed"/>
		/// are <see langword="false"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Error"/> if both <paramref name="isFinished"/> and <paramref name="isFailed"/>
		/// are <see langword="true"/>.
		/// </para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status FinishedFailedToStatus(bool isFinished, bool isFailed, bool isValid)
		{
			Status trueStatus = FinishedFailedToStatusSimple(isFinished, isFailed);
			return ConditionToStatus(!(isFinished & isFailed) & isValid, Status.Error, trueStatus);
		}

		/// <summary>
		/// Converts <paramref name="condition"/> into a <see cref="Status"/> value.
		/// </summary>
		/// <param name="condition">Value input.</param>
		/// <returns>
		/// <para><see cref="Status.Success"/> if <paramref name="condition"/> is <see langword="true"/>.</para>
		/// <para><see cref="Status.Failure"/> if <paramref name="condition"/> is <see langword="false"/>.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status ConditionToStatus(bool condition)
		{
			return (Status)((int)Status.Failure >> (*(byte*)&condition << 1));
		}

		/// <summary>
		/// Converts <paramref name="condition"/> into a <see cref="Status"/> value with a validity check.
		/// </summary>
		/// <param name="condition">Value input.</param>
		/// <param name="isValid">Validity input.</param>
		/// <returns>
		/// <para>
		/// <see cref="Status.Success"/> if <paramref name="condition"/> is <see langword="true"/>
		/// and <paramref name="isValid"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Failure"/> if <paramref name="condition"/> is <see langword="false"/>
		/// and <paramref name="isValid"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Error"/> if <paramref name="isValid"/> is <see langword="false"/>.
		/// <paramref name="condition"/> can be any in this case.
		/// </para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static Status ConditionToStatus(bool condition, bool isValid)
		{
			return ConditionToStatus(isValid, Status.Error, ConditionToStatus(condition));
		}

		/// <summary>
		/// Converts <paramref name="condition"/> into a <see cref="Status"/> value.
		/// </summary>
		/// <param name="condition">Value input.</param>
		/// <param name="falseStatus">Return value when <paramref name="condition"/> is <see langword="false"/>.</param>
		/// <param name="trueStatus">Return value when <paramref name="condition"/> is <see langword="true"/>.</param>
		/// <returns>
		/// <para><paramref name="trueStatus"/> if <paramref name="condition"/> is <see langword="true"/>.</para>
		/// <para><paramref name="falseStatus"/> if <paramref name="condition"/> is <see langword="false"/>.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static unsafe Status ConditionToStatus(bool condition, Status falseStatus, Status trueStatus)
		{
			int conditionMask = -(*(byte*)&condition);
			return (Status)(((int)trueStatus & conditionMask) | ((int)falseStatus & ~conditionMask));
		}

		/// <summary>
		/// Converts <paramref name="isFinished"/> and <paramref name="isFailed"/> into a <see cref="Status"/> value.
		/// </summary>
		/// <param name="isFinished">Finished input.</param>
		/// <param name="isFailed">Failed input.</param>
		/// <returns>
		/// <para>
		/// <see cref="Status.Success"/> if <paramref name="isFinished"/> is <see langword="true"/>
		/// and <paramref name="isFailed"/> is <see langword="false"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Failure"/> if <paramref name="isFinished"/> is <see langword="false"/>
		/// and <paramref name="isFailed"/> is <see langword="true"/>.
		/// </para>
		/// <para>
		/// <see cref="Status.Running"/> if both <paramref name="isFinished"/> and <paramref name="isFailed"/>
		/// are <see langword="true"/> or <see langword="false"/>.
		/// </para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		private static unsafe Status FinishedFailedToStatusSimple(bool isFinished, bool isFailed)
		{
			return (Status)((int)Status.Running >> *(byte*)&isFinished << *(byte*)&isFailed);
		}
	}
}
