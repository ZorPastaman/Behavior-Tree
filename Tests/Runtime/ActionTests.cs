// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using NUnit.Framework;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Actions;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Tests
{
	public static class ActionTests
	{
		[Test]
		public static void RemoveClassValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const string value = "value";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<RemoveClassValue<string>>(propertyName).Finish();
			Tree tree = treeBuilder.Build(blackboard);
			tree.Initialize();

			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<string>(propertyName));

			tree.Dispose();
		}

		[Test]
		public static void RemoveStructValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const int value = 3;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<RemoveStructValue<int>>(propertyName).Finish();
			Tree tree = treeBuilder.Build(blackboard);
			tree.Initialize();

			blackboard.SetStructValue(propertyName, value);
			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<int>(propertyName));

			tree.Dispose();
		}

		[Test]
		public static void SetClassValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const string value = "value";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<SetClassValue<string>>(value, propertyName).Finish();
			Tree tree = treeBuilder.Build(blackboard);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(propertyName, out string val));
			Assert.AreEqual(value, val);

			tree.Dispose();
		}

		[Test]
		public static void SetStructValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const int value = -2;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<SetStructValue<int>>(value, propertyName).Finish();
			Tree tree = treeBuilder.Build(blackboard);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(propertyName, out int val));
			Assert.AreEqual(value, val);

			tree.Dispose();
		}
	}
}
