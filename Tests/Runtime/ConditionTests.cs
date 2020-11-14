// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using NUnit.Framework;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Conditions;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Tests
{
	public static class ConditionTests
	{
		[Test]
		public static void HasClassValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const string value = "value";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<HasClassValue<string>>(propertyName).Finish();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void HasStructValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const int value = 3;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<HasStructValue<int>>(propertyName).Finish();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsClassEqualTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			string value = "value";
			var blackboard = new Blackboard();
			blackboard.SetClassValue(propertyName, value);
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<IsClassEqual<string>>(value, propertyName).Finish();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = "valueChanged";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.RemoveObject(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsStructEqualTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			int value = 0;
			var blackboard = new Blackboard();
			blackboard.SetStructValue(propertyName, value);
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<IsStructEqual<int>>(value, propertyName).Finish();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = 1;
			blackboard.SetStructValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.RemoveObject(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}
	}
}
