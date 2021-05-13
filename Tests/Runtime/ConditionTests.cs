// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using NUnit.Framework;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves.Conditions;
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
			treeBuilder.AddLeaf<HasClassValue<string>, BlackboardPropertyName>(propertyName).Complete();
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
			treeBuilder.AddLeaf<HasStructValue<int>, BlackboardPropertyName>(propertyName).Complete();
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
			treeBuilder.AddLeaf<IsClassValueEqual<string>, string, BlackboardPropertyName>(value, propertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = "valueChanged";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.RemoveObject(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			value = null;
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueEqual<string>, string, BlackboardPropertyName>(value, propertyName).Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = "valueChanged";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsClassEqualVariableTest()
		{
			var firstPropertyName = new BlackboardPropertyName("first");
			var secondPropertyName = new BlackboardPropertyName("second");
			const string firstValue = "firstValue";
			const string secondValue = "secondValue";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueEqualVariable<string>, BlackboardPropertyName, BlackboardPropertyName>(firstPropertyName, secondPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveObject<string>(firstPropertyName);
			blackboard.SetClassValue(secondPropertyName, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(secondPropertyName, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetClassValue<string>(secondPropertyName, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue<string>(firstPropertyName, null);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsClassGreaterTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			string value = "value";
			var blackboard = new Blackboard();
			blackboard.SetClassValue(propertyName, value);
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueGreater<string>, string, BlackboardPropertyName>(value, propertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = "val";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = "valueChanged";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = null;
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueGreater<string>, string, BlackboardPropertyName>(null, propertyName).Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = "value";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = null;
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.RemoveObject<string>(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsClassGreaterVariableTest()
		{
			var firstPropertyName = new BlackboardPropertyName("first");
			var secondPropertyName = new BlackboardPropertyName("second");
			const string firstValue = "firstValue";
			const string secondValue = "secondValue";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueGreaterVariable<string>, BlackboardPropertyName, BlackboardPropertyName>(firstPropertyName, secondPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveObject<string>(firstPropertyName);
			blackboard.SetClassValue(secondPropertyName, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(secondPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, secondValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetClassValue<string>(firstPropertyName, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue<string>(secondPropertyName, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsClassLessTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			string value = "value";
			var blackboard = new Blackboard();
			blackboard.SetClassValue(propertyName, value);
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueLess<string>, string, BlackboardPropertyName>(value, propertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = "val";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = "valueChanged";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = null;
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueLess<string>, string, BlackboardPropertyName>(null, propertyName).Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = "value";
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = null;
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.RemoveObject<string>(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsClassLessVariableTest()
		{
			var firstPropertyName = new BlackboardPropertyName("first");
			var secondPropertyName = new BlackboardPropertyName("second");
			const string firstValue = "firstValue";
			const string secondValue = "secondValue";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueLessVariable<string>, BlackboardPropertyName, BlackboardPropertyName>(firstPropertyName, secondPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveObject<string>(firstPropertyName);
			blackboard.SetClassValue(secondPropertyName, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetClassValue(secondPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, secondValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue<string>(firstPropertyName, null);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetClassValue<string>(secondPropertyName, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

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
			treeBuilder.AddLeaf<IsStructValueEqual<int>, int, BlackboardPropertyName>(value, propertyName).Complete();
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

		[Test]
		public static void IsStructEqualVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("firstProperty");
			var secondProperty = new BlackboardPropertyName("secondProperty");
			const int firstValue = 0;
			const int secondValue = 1;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsStructValueEqualVariable<int>, BlackboardPropertyName, BlackboardPropertyName>(firstProperty, secondProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveStruct<int>(firstProperty);
			blackboard.SetStructValue(secondProperty, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(secondProperty, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, secondValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsStructGreater()
		{
			var propertyName = new BlackboardPropertyName("value");
			int value = 0;
			var blackboard = new Blackboard();
			blackboard.SetStructValue(propertyName, value);
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsStructValueGreater<int>, int, BlackboardPropertyName>(value, propertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = 10;
			blackboard.SetStructValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = -10;
			blackboard.SetStructValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.RemoveStruct<int>(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsStructGreaterVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("firstProperty");
			var secondProperty = new BlackboardPropertyName("secondProperty");
			const int firstValue = 0;
			const int secondValue = 1;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsStructValueGreaterVariable<int>, BlackboardPropertyName, BlackboardPropertyName>(firstProperty, secondProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveStruct<int>(firstProperty);
			blackboard.SetStructValue(secondProperty, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(secondProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, secondValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsStructLess()
		{
			var propertyName = new BlackboardPropertyName("value");
			int value = 0;
			var blackboard = new Blackboard();
			blackboard.SetStructValue(propertyName, value);
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsStructValueLess<int>, int, BlackboardPropertyName>(value, propertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = 10;
			blackboard.SetStructValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = -10;
			blackboard.SetStructValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.RemoveStruct<int>(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsStructLessVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("firstProperty");
			var secondProperty = new BlackboardPropertyName("secondProperty");
			const int firstValue = 0;
			const int secondValue = 1;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsStructValueLessVariable<int>, BlackboardPropertyName, BlackboardPropertyName>(firstProperty, secondProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveStruct<int>(firstProperty);
			blackboard.SetStructValue(secondProperty, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(secondProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, secondValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
	}
}
