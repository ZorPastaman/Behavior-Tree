// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core
{
	public interface ISetupable<in TArg>
	{
		void Setup(TArg arg);
	}

	public interface ISetupable<in TArg0, in TArg1>
	{
		void Setup(TArg0 arg0, TArg1 arg1);
	}

	public interface ISetupable<in TArg0, in TArg1, in TArg2>
	{
		void Setup(TArg0 arg0, TArg1 arg1, TArg2 arg2);
	}

	public interface ISetupable<in TArg0, in TArg1, in TArg2, in TArg3>
	{
		void Setup(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3);
	}

	public interface ISetupable<in TArg0, in TArg1, in TArg2, in TArg3, in TArg4>
	{
		void Setup(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
	}

	public interface ISetupable<in TArg0, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5>
	{
		void Setup(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5);
	}

	public interface ISetupable<in TArg0, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, in TArg6>
	{
		void Setup(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6);
	}

	public interface ISetupable<in TArg0, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, in TArg6, in TArg7>
	{
		void Setup(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7);
	}

	public interface INotSetupable
	{
	}
}
