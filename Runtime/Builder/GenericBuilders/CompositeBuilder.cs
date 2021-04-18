// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Composite-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;

namespace Zor.BehaviorTree.Builder.GenericBuilders
{
	internal sealed class CompositeBuilder<TComposite> : CompositeBuilder
		where TComposite : Composite, INotSetupable, new()
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite>(children);
		}
	}

	internal sealed class CompositeBuilder<TComposite, TArg> : CompositeBuilder
		where TComposite : Composite, ISetupable<TArg>, new()
	{
		private readonly TArg m_arg;

		public CompositeBuilder(TArg arg)
		{
			m_arg = arg;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite, TArg>(children, m_arg);
		}
	}

	internal sealed class CompositeBuilder<TComposite, TArg0, TArg1> : CompositeBuilder
		where TComposite : Composite, ISetupable<TArg0, TArg1>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;

		public CompositeBuilder(TArg0 arg0, TArg1 arg1)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite, TArg0, TArg1>(children, m_arg0, m_arg1);
		}
	}

	internal sealed class CompositeBuilder<TComposite, TArg0, TArg1, TArg2> : CompositeBuilder
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;

		public CompositeBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite, TArg0, TArg1, TArg2>(children, m_arg0, m_arg1, m_arg2);
		}
	}

	internal sealed class CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3> : CompositeBuilder
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;

		public CompositeBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite, TArg0, TArg1, TArg2, TArg3>(children,
				m_arg0, m_arg1, m_arg2, m_arg3);
		}
	}

	internal sealed class CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4> : CompositeBuilder
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;

		public CompositeBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4>(children,
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4);
		}
	}

	internal sealed class CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> : CompositeBuilder
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;

		public CompositeBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(children,
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5);
		}
	}

	internal sealed class CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : CompositeBuilder
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;
		private readonly TArg6 m_arg6;

		public CompositeBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
			m_arg6 = arg6;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(children,
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5, m_arg6);
		}
	}

	internal sealed class CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		CompositeBuilder
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;
		private readonly TArg6 m_arg6;
		private readonly TArg7 m_arg7;

		public CompositeBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6,
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

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(children,
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5, m_arg6, m_arg7);
		}
	}
}
