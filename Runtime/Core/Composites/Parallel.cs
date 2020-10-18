// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Composites
{
	[UsedImplicitly, Preserve]
	public sealed class Parallel : Composite
	{
		private readonly Mode m_mode;

		public Parallel([NotNull] Blackboard blackboard, [NotNull] Behavior[] children, Mode mode)
			: base(blackboard, children)
		{
			m_mode = mode;
		}

		protected override Status Execute()
		{
			int childrenCount = children.Length;
			int successes = 0;
			int failures = 0;

			for (int i = 0; i < childrenCount; ++i)
			{
				Status childStatus = children[i].Tick();

				if (childStatus == Status.Success)
				{
					++successes;

					if (m_mode == Mode.Any)
					{
						return Status.Success;
					}
				}
				else if (childStatus == Status.Failure)
				{
					++failures;

					if (m_mode == Mode.Any)
					{
						return Status.Failure;
					}
				}
				else if (childStatus != Status.Running)
				{
					return childStatus;
				}
			}

			if (m_mode == Mode.All)
			{
				if (successes == childrenCount)
				{
					return Status.Success;
				}

				if (failures == childrenCount)
				{
					return Status.Failure;
				}
			}

			return Status.Running;
		}

		protected override void End()
		{
			base.End();

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Abort();
			}
		}

		[Flags]
		public enum Mode : byte
		{
			Any = 1 << 0,
			All = 1 << 1
		}
	}
}
