// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves.Actions;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	public abstract class SerializedAction<TAction> : SerializedLeaf<TAction>
		where TAction : Action, INotSetupable, new()
	{
	}
	
	public abstract class SerializedAction<TAction, TArg> : SerializedLeaf<TAction, TArg>
		where TAction : Action, ISetupable<TArg>, new()
	{
	}
	
	public abstract class SerializedAction<TAction, TArg0, TArg1> :
		SerializedLeaf<TAction, TArg0, TArg1>
		where TAction : Action, ISetupable<TArg0, TArg1>, new()
	{
	}
	
	public abstract class SerializedAction<TAction, TArg0, TArg1, TArg2> :
		SerializedLeaf<TAction, TArg0, TArg1, TArg2>
		where TAction : Action, ISetupable<TArg0, TArg1, TArg2>, new()
	{
	}
	
	public abstract class SerializedAction<TAction, TArg0, TArg1, TArg2, TArg3> :
		SerializedLeaf<TAction, TArg0, TArg1, TArg2, TArg3>
		where TAction : Action, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
	{
	}
	
	public abstract class SerializedAction<TAction, TArg0, TArg1, TArg2, TArg3, TArg4> :
		SerializedLeaf<TAction, TArg0, TArg1, TArg2, TArg3, TArg4>
		where TAction : Action, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
	{
	}
	
	public abstract class SerializedAction<TAction, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> :
		SerializedLeaf<TAction, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>
		where TAction : Action, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
	{
	}
	
	public abstract class SerializedAction<TAction, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> :
		SerializedLeaf<TAction, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
		where TAction : Action, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
	{
	}
	
	public abstract class SerializedAction<TAction,
		TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		SerializedLeaf<TAction, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
		where TAction : Action,
		ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
	{
	}
}
