﻿// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;

namespace Zor.BehaviorTree.Core
{
	[Flags]
	public enum Status : byte
	{
		Idle = 1 << 0,
		Success = 1 << 1,
		Running = 1 << 2,
		Failure = 1 << 3,
		Abort = 1 << 4,
		Error = 1 << 5
	}
}
