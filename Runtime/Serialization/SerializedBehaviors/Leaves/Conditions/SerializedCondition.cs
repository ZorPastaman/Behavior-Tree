// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves.Conditions;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	public abstract class SerializedCondition<TCondition> : SerializedLeaf<TCondition>
		where TCondition : Condition, INotSetupable, new()
	{
	}
	
	public abstract class SerializedCondition<TCondition, TArg> : SerializedLeaf<TCondition, TArg>
		where TCondition : Condition, ISetupable<TArg>, new()
	{
	}
	
	public abstract class SerializedCondition<TCondition, TArg0, TArg1> :
		SerializedLeaf<TCondition, TArg0, TArg1>
		where TCondition : Condition, ISetupable<TArg0, TArg1>, new()
	{
	}
	
	public abstract class SerializedCondition<TCondition, TArg0, TArg1, TArg2> :
		SerializedLeaf<TCondition, TArg0, TArg1, TArg2>
		where TCondition : Condition, ISetupable<TArg0, TArg1, TArg2>, new()
	{
	}
	
	public abstract class SerializedCondition<TCondition, TArg0, TArg1, TArg2, TArg3> :
		SerializedLeaf<TCondition, TArg0, TArg1, TArg2, TArg3>
		where TCondition : Condition, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
	{
	}
	
	public abstract class SerializedCondition<TCondition, TArg0, TArg1, TArg2, TArg3, TArg4> :
		SerializedLeaf<TCondition, TArg0, TArg1, TArg2, TArg3, TArg4>
		where TCondition : Condition, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
	{
	}
	
	public abstract class SerializedCondition<TCondition, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> :
		SerializedLeaf<TCondition, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>
		where TCondition : Condition, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
	{
	}
	
	public abstract class SerializedCondition<TCondition, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> :
		SerializedLeaf<TCondition, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
		where TCondition : Condition, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
	{
	}
	
	public abstract class SerializedCondition<TCondition,
		TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		SerializedLeaf<TCondition, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
		where TCondition : Condition,
		ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
	{
	}
}
