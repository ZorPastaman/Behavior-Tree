// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.SimpleBlackboard.Core;
using WaitForSeconds = Zor.BehaviorTree.Core.Leaves.Conditions.WaitForSeconds;

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

			blackboard.RemoveObject<string>(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

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

			blackboard.RemoveObject<string>(propertyName);
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

		[UnityTest]
		public static IEnumerator WaitFramesTest()
		{
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<WaitForFrames, int>(3).Complete();
			TreeRoot treeRoot = treeBuilder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return null;
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return null;
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator WaitSecondsTest()
		{
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<WaitForSeconds, float>(3f).Complete();
			TreeRoot treeRoot = treeBuilder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return new UnityEngine.WaitForSeconds(1f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return new UnityEngine.WaitForSeconds(3f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			yield return new UnityEngine.WaitForSeconds(1f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			yield return new UnityEngine.WaitForSeconds(3f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
	}
}
