// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.SimpleBlackboard.Core;
using WaitForSeconds = Zor.BehaviorTree.Core.Leaves.Actions.WaitForSeconds;

namespace Zor.BehaviorTree.Tests
{
	public static class ActionTests
	{
		[Test]
		public static void CopyClassValueTest()
		{
			var sourcePropertyName = new BlackboardPropertyName("value");
			var destinationPropertyName = new BlackboardPropertyName("copied");
			const string value = "value";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CopyClassValue<string>, BlackboardPropertyName, BlackboardPropertyName>(
				sourcePropertyName, destinationPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(sourcePropertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(destinationPropertyName, out string copiedValue));
			Assert.AreEqual(value, copiedValue);

			blackboard.SetClassValue<string>(sourcePropertyName, null);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(destinationPropertyName, out copiedValue));
			Assert.AreEqual(null, copiedValue);

			treeRoot.Dispose();
		}

		[Test]
		public static void CopyStructValueTest()
		{
			var sourcePropertyName = new BlackboardPropertyName("value");
			var destinationPropertyName = new BlackboardPropertyName("copied");
			const int value = 3;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CopyStructValue<int>, BlackboardPropertyName, BlackboardPropertyName>(
				sourcePropertyName, destinationPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(sourcePropertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(destinationPropertyName, out int copiedValue));
			Assert.AreEqual(value, copiedValue);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetBehaviourEnabledTest()
		{
			var behaviourProperty = new BlackboardPropertyName("behaviour");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();
			var behaviour = new GameObject().AddComponent<Camera>();
			behaviour.enabled = false;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<GetBehaviourEnabled, BlackboardPropertyName, BlackboardPropertyName>(behaviourProperty,
				valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(valueProperty));

			blackboard.SetClassValue<Behaviour>(behaviourProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(valueProperty));

			blackboard.SetClassValue(behaviourProperty, behaviour);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(valueProperty, out bool value));
			Assert.IsFalse(value);

			behaviour.enabled = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(valueProperty, out value));
			Assert.IsTrue(value);

			treeRoot.Dispose();
		}

		[Test]
		public static void RemoveClassValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const string value = "value";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<RemoveClassValue<string>, BlackboardPropertyName>(propertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<string>(propertyName));

			treeRoot.Dispose();
		}

		[Test]
		public static void RemoveStructValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const int value = 3;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<RemoveStructValue<int>, BlackboardPropertyName>(propertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			blackboard.SetStructValue(propertyName, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<int>(propertyName));

			treeRoot.Dispose();
		}

		[Test]
		public static void SetBehaviourEnabledTest()
		{
			var behaviourProperty = new BlackboardPropertyName("behaviour");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetBehaviourEnabled, BlackboardPropertyName, bool>(behaviourProperty, false).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Behaviour>(behaviourProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Behaviour behaviour = new GameObject().AddComponent<Camera>();
			blackboard.SetClassValue(behaviourProperty, behaviour);
			behaviour.enabled = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsFalse(behaviour.enabled);

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsFalse(behaviour.enabled);

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetBehaviourEnabled, BlackboardPropertyName, bool>(behaviourProperty, true).Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			behaviour.enabled = false;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(behaviour.enabled);

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(behaviour.enabled);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetBehaviourEnabledVariableTest()
		{
			var behaviourProperty = new BlackboardPropertyName("behaviour");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
			.AddLeaf<SetBehaviourEnabledVariable, BlackboardPropertyName, BlackboardPropertyName>(behaviourProperty, valueProperty)
			.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Behaviour>(behaviourProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Behaviour behaviour = new GameObject().AddComponent<Camera>();
			blackboard.SetClassValue(behaviourProperty, behaviour);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Behaviour>(behaviourProperty, null);
			blackboard.SetStructValue(valueProperty, false);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(behaviourProperty, behaviour);
			behaviour.enabled = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsFalse(behaviour.enabled);

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsFalse(behaviour.enabled);

			blackboard.SetStructValue(valueProperty, true);
			behaviour.enabled = false;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(behaviour.enabled);

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(behaviour.enabled);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetClassValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			string value = "value";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetClassValue<string>, string, BlackboardPropertyName>(value, propertyName)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(propertyName, out string val));
			Assert.AreEqual(value, val);

			treeRoot.Dispose();

			value = null;
			blackboard = new Blackboard();
			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetClassValue<string>, string, BlackboardPropertyName>(value, propertyName)
				.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(propertyName, out val));
			Assert.AreEqual(value, val);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetStructValueTest()
		{
			var propertyName = new BlackboardPropertyName("value");
			const int value = -2;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetStructValue<int>, int, BlackboardPropertyName>(value, propertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(propertyName, out int val));
			Assert.AreEqual(value, val);

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator WaitForFramesTest()
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

		[Test]
		public static void WaitForFramesBlackboardTest()
		{
			var frames = new BlackboardPropertyName("frames");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<WaitForFramesBlackboard, BlackboardPropertyName, int>(frames, 3).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			int frame = 0;
			blackboard.SetStructValue(frames, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frames, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frames, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frames, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frames, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frames, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frames, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frames, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator WaitForFramesVariableTest()
		{
			var property = new BlackboardPropertyName("property");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<WaitForFramesVariable, BlackboardPropertyName>(property).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(property, 3);
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

		[Test]
		public static void WaitForFramesVariableBlackboardTest()
		{
			var property = new BlackboardPropertyName("property");
			var frameProperty = new BlackboardPropertyName("frame");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<WaitForFramesVariableBlackboard, BlackboardPropertyName, BlackboardPropertyName>(
				frameProperty, property).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(property, 3);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			int frame = 0;
			blackboard.SetStructValue(frameProperty, frame);
			blackboard.RemoveStruct<int>(property);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(property, 3);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator WaitForSecondsTest()
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

		[Test]
		public static void WaitForSecondsBlackboardTest()
		{
			var secondsProperty = new BlackboardPropertyName("seconds");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<WaitForSecondsBlackboard, BlackboardPropertyName, float>(secondsProperty, 3f).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			float seconds = 0f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator WaitForSecondsVariableTest()
		{
			var property = new BlackboardPropertyName("property");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<WaitForSecondsVariable, BlackboardPropertyName>(property).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(property, 3f);
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

		[Test]
		public static void WaitForSecondsVariableBlackboardTest()
		{
			var property = new BlackboardPropertyName("property");
			var secondsProperty = new BlackboardPropertyName("seconds");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<WaitForSecondsVariableBlackboard, BlackboardPropertyName, BlackboardPropertyName>(
				secondsProperty, property).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(property, 3f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			float seconds = 0f;
			blackboard.SetStructValue(secondsProperty, seconds);
			blackboard.RemoveStruct<float>(property);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(property, 3f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
	}
}
