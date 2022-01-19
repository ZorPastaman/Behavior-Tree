// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;

namespace Zor.BehaviorTree.Core
{
	/// <summary>
	/// Status of a <see cref="Behavior"/>.
	/// </summary>
	/// <seealso cref="Behavior.status"/>
	[Flags]
	public enum Status : byte
	{
		/// <summary>
		/// A <see cref="Behavior"/> was never ticked.
		/// </summary>
		Idle = 1 << 0,
		/// <summary>
		/// A tick of a <see cref="Behavior"/> was finished successfully.
		/// </summary>
		Success = 1 << 1,
		/// <summary>
		/// A tick of a <see cref="Behavior"/> did something but it's not finished and it's needed to tick again.
		/// </summary>
		/// <remarks>
		/// Usually, it's returned by behaviors that have continuous executes.
		/// </remarks>
		Running = 1 << 2,
		/// <summary>
		/// A tick of a <see cref="Behavior"/> was finished unsuccessfully.
		/// </summary>
		Failure = 1 << 3,
		/// <summary>
		/// A tick of a <see cref="Behavior"/> had an error that prevented it from a correct execution.
		/// </summary>
		/// <remarks>
		/// Usually, it means that a <see cref="Behavior"/> didn't have necessary data or
		/// a world was in an incorrect state.
		/// </remarks>
		Error = 1 << 4,
		/// <summary>
		/// An execution of a <see cref="Behavior"/> was aborted and not ticked since then.
		/// </summary>
		Abort = 1 << 5
	}
}
