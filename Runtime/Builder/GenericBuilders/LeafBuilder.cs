// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves;

namespace Zor.BehaviorTree.Builder.GenericBuilders
{
	internal sealed class LeafBuilder<TLeaf> : LeafBuilder where TLeaf : Leaf, INotSetupable, new()
	{
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf>();
		}
	}

	internal sealed class LeafBuilder<TLeaf, TArg> : LeafBuilder where TLeaf : Leaf, ISetupable<TArg>, new()
	{
		private readonly TArg m_arg;

		public LeafBuilder(TArg arg)
		{
			m_arg = arg;
		}

		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf, TArg>(m_arg);
		}
	}

	internal sealed class LeafBuilder<TLeaf, TArg0, TArg1> : LeafBuilder
		where TLeaf : Leaf, ISetupable<TArg0, TArg1>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;

		public LeafBuilder(TArg0 arg0, TArg1 arg1)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
		}

		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf, TArg0, TArg1>(m_arg0, m_arg1);
		}
	}

	internal sealed class LeafBuilder<TLeaf, TArg0, TArg1, TArg2> : LeafBuilder
		where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;

		public LeafBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
		}

		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf, TArg0, TArg1, TArg2>(m_arg0, m_arg1, m_arg2);
		}
	}

	internal sealed class LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3> : LeafBuilder
		where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;

		public LeafBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
		}

		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf, TArg0, TArg1, TArg2, TArg3>(m_arg0, m_arg1, m_arg2, m_arg3);
		}
	}

	internal sealed class LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4> : LeafBuilder
		where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;

		public LeafBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
		}

		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4>(
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4);
		}
	}

	internal sealed class LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> : LeafBuilder
		where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;

		public LeafBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
		}

		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5);
		}
	}

	internal sealed class LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : LeafBuilder
		where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;
		private readonly TArg6 m_arg6;

		public LeafBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
			m_arg6 = arg6;
		}

		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5, m_arg6);
		}
	}

	internal sealed class LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		LeafBuilder
		where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;
		private readonly TArg6 m_arg6;
		private readonly TArg7 m_arg7;

		public LeafBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6,
			TArg7 arg7)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
			m_arg6 = arg6;
			m_arg7 = arg7;
		}

		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TLeaf);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5, m_arg6, m_arg7);
		}
	}
}
