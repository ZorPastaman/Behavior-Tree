// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves.StatusBehaviors;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.StatusBehaviors
{
	public abstract class SerializedStatusBehavior<TStatusBehavior> : SerializedLeaf<TStatusBehavior>
		where TStatusBehavior : StatusBehavior, INotSetupable, new()
	{
	}
	
	public abstract class SerializedStatusBehavior<TStatusBehavior, TArg> : SerializedLeaf<TStatusBehavior, TArg>
		where TStatusBehavior : StatusBehavior, ISetupable<TArg>, new()
	{
	}
	
	public abstract class SerializedStatusBehavior<TStatusBehavior, TArg0, TArg1> :
		SerializedLeaf<TStatusBehavior, TArg0, TArg1>
		where TStatusBehavior : StatusBehavior, ISetupable<TArg0, TArg1>, new()
	{
	}
	
	public abstract class SerializedStatusBehavior<TStatusBehavior, TArg0, TArg1, TArg2> :
		SerializedLeaf<TStatusBehavior, TArg0, TArg1, TArg2>
		where TStatusBehavior : StatusBehavior, ISetupable<TArg0, TArg1, TArg2>, new()
	{
	}
	
	public abstract class SerializedStatusBehavior<TStatusBehavior, TArg0, TArg1, TArg2, TArg3> :
		SerializedLeaf<TStatusBehavior, TArg0, TArg1, TArg2, TArg3>
		where TStatusBehavior : StatusBehavior, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
	{
	}
	
	public abstract class SerializedStatusBehavior<TStatusBehavior, TArg0, TArg1, TArg2, TArg3, TArg4> :
		SerializedLeaf<TStatusBehavior, TArg0, TArg1, TArg2, TArg3, TArg4>
		where TStatusBehavior : StatusBehavior, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
	{
	}
	
	public abstract class SerializedStatusBehavior<TStatusBehavior, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> :
		SerializedLeaf<TStatusBehavior, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>
		where TStatusBehavior : StatusBehavior, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
	{
	}
	
	public abstract class SerializedStatusBehavior<TStatusBehavior, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> :
		SerializedLeaf<TStatusBehavior, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
		where TStatusBehavior : StatusBehavior, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
	{
	}
	
	public abstract class SerializedStatusBehavior<TStatusBehavior,
		TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		SerializedLeaf<TStatusBehavior, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
		where TStatusBehavior : StatusBehavior,
		ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
	{
	}
}
