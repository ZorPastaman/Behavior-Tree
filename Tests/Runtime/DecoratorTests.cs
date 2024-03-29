// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.Leaves.StatusBehaviors;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Tests
{
	public static class DecoratorTests
	{
		[UnityTest]
		public static IEnumerator CooldownOfFramesTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("status");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<CooldownOfFrames, int>(3)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CooldownOfFramesBlackboardTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("status");
			var frameProperty = new BlackboardPropertyName("frame");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<CooldownOfFramesBlackboard, BlackboardPropertyName, int>(frameProperty, 3)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			int frame = 0;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator CooldownOfFramesVariableTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var statusProperty = new BlackboardPropertyName("status");
			var durationProperty = new BlackboardPropertyName("duration");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<CooldownOfFramesVariable, BlackboardPropertyName>(durationProperty)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(statusProperty).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(statusProperty, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(durationProperty, 3);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CooldownOfFramesVariableBlackboardTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var statusProperty = new BlackboardPropertyName("status");
			var durationProperty = new BlackboardPropertyName("duration");
			var frameProperty = new BlackboardPropertyName("frame");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<CooldownOfFramesVariableBlackboard, BlackboardPropertyName, BlackboardPropertyName>(
					frameProperty, durationProperty)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(statusProperty).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(statusProperty, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			int frame = 0;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(durationProperty, 3);
			blackboard.RemoveStruct<int>(frameProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator CooldownOfSecondsTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("status");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<CooldownOfSeconds, float>(3f)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CooldownOfSecondsBlackboardTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("status");
			var secondsProperty = new BlackboardPropertyName("seconds");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<CooldownOfSecondsBlackboard, BlackboardPropertyName, float>(secondsProperty, 3f)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			float seconds = 0f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator CooldownOfSecondsVariableTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var statusProperty = new BlackboardPropertyName("status");
			var durationProperty = new BlackboardPropertyName("duration");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<CooldownOfSecondsVariable, BlackboardPropertyName>(durationProperty)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(statusProperty).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(statusProperty, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(durationProperty, 3f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CooldownOfSecondsVariableBlackboardTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var statusProperty = new BlackboardPropertyName("status");
			var durationProperty = new BlackboardPropertyName("duration");
			var secondsProperty = new BlackboardPropertyName("seconds");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<CooldownOfSecondsVariableBlackboard, BlackboardPropertyName, BlackboardPropertyName>(
					secondsProperty, durationProperty)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(statusProperty).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(statusProperty, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			float seconds = 0f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<float>(secondsProperty);
			blackboard.SetStructValue(durationProperty, 3f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(statusProperty, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void InverterTest()
		{
			var builder = new TreeBuilder();
			builder.AddDecorator<Inverter>()
				.AddLeaf<SuccessBehavior>().Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Inverter>()
				.AddLeaf<RunningBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Inverter>()
				.AddLeaf<FailureBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Inverter>()
				.AddLeaf<ErrorBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator LimitOfFramesTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<LimitOfFrames, int>(3)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void LimitOfFramesBlackboardTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("value");
			var frameProperty = new BlackboardPropertyName("frame");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<LimitOfFramesBlackboard, BlackboardPropertyName, int>(frameProperty, 3)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			int frame = 0;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator LimitOfFramesVariableTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("value");
			var durationProperty = new BlackboardPropertyName("duration");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<LimitOfFramesVariable, BlackboardPropertyName>(durationProperty)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(durationProperty, 3);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return null;
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return null;
			yield return null;
			yield return null;
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void LimitOfFramesVariableBlackboardTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("value");
			var durationProperty = new BlackboardPropertyName("duration");
			var frameProperty = new BlackboardPropertyName("frame");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<LimitOfFramesVariableBlackboard, BlackboardPropertyName, BlackboardPropertyName>(
					frameProperty, durationProperty)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			int frame = 0;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(durationProperty, 3);
			blackboard.RemoveStruct<int>(frameProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			++frame;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			frame += 3;
			blackboard.SetStructValue(frameProperty, frame);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator LimitOfSecondsTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<LimitOfSeconds, float>(3f)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return new WaitForSeconds(4f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void LimitOfSecondsBlackboardTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var property = new BlackboardPropertyName("value");
			var secondsProperty = new BlackboardPropertyName("seconds");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<LimitOfSecondsBlackboard, BlackboardPropertyName, float>(secondsProperty, 3f)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(property).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(property, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			float seconds = 0f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			seconds += 4f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);

			blackboard.SetStructValue(property, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(property, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[UnityTest]
		public static IEnumerator LimitOfSecondsVariableTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var valueProperty = new BlackboardPropertyName("value");
			var durationProperty = new BlackboardPropertyName("duration");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<LimitOfSecondsVariable, BlackboardPropertyName>(durationProperty)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(valueProperty).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(valueProperty, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(durationProperty, 3f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			yield return new WaitForSeconds(4f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(valueProperty, Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);

			blackboard.SetStructValue(valueProperty, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(valueProperty, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return new WaitForSeconds(1f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			yield return new WaitForSeconds(3f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void LimitOfSecondsVariableBlackboardTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childFieldName = "child";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childField = typeof(Decorator).GetField(childFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var valueProperty = new BlackboardPropertyName("value");
			var durationProperty = new BlackboardPropertyName("duration");
			var secondsProperty = new BlackboardPropertyName("seconds");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<LimitOfSecondsVariableBlackboard, BlackboardPropertyName, BlackboardPropertyName>(
					secondsProperty, durationProperty)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(valueProperty).Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var child = (Behavior)childField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(valueProperty, Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			float seconds = 0f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(durationProperty, 3f);
			blackboard.RemoveStruct<float>(secondsProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			seconds += 4f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(valueProperty, Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreNotEqual(Status.Running, child.status);

			blackboard.SetStructValue(valueProperty, Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(valueProperty, Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			seconds += 1f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			seconds += 3f;
			blackboard.SetStructValue(secondsProperty, seconds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void RepeaterTest()
		{
			var builder = new TreeBuilder();
			builder.AddDecorator<Repeater, uint>(3u)
				.AddLeaf<SuccessBehavior>().Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Repeater, uint>(3u)
				.AddLeaf<RunningBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Repeater, uint>(3u)
				.AddLeaf<FailureBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Repeater, uint>(3u)
				.AddLeaf<ErrorBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			treeRoot.Dispose();
		}

		[Test]
		public static void RepeaterVariableTest()
		{
			var repeatsProperty = new BlackboardPropertyName("repeats");
			var blackboard = new Blackboard();
			var builder = new TreeBuilder();
			builder.AddDecorator<RepeaterVariable, BlackboardPropertyName>(repeatsProperty)
				.AddLeaf<SuccessBehavior>().Complete()
				.Complete();
			TreeRoot treeRoot = builder.Build(blackboard);
			treeRoot.Initialize();
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			treeRoot.Dispose();

			blackboard.SetStructValue(repeatsProperty, 3u);
			builder = new TreeBuilder();
			builder.AddDecorator<RepeaterVariable, BlackboardPropertyName>(repeatsProperty)
				.AddLeaf<SuccessBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build(blackboard);
			treeRoot.Initialize();
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Repeater, uint>(3u)
				.AddLeaf<RunningBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build(blackboard);
			treeRoot.Initialize();
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Repeater, uint>(3u)
				.AddLeaf<FailureBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build(blackboard);
			treeRoot.Initialize();
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Repeater, uint>(3u)
				.AddLeaf<ErrorBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build(blackboard);
			treeRoot.Initialize();
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			treeRoot.Dispose();
		}

		[Test]
		public static void UntilTest()
		{
			var builder = new TreeBuilder();
			builder.AddDecorator<Until>()
				.AddLeaf<SuccessBehavior>().Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Until>()
				.AddLeaf<FailureBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Until>()
				.AddLeaf<RunningBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Until>()
				.AddLeaf<ErrorBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void WhileTest()
		{
			var builder = new TreeBuilder();
			builder.AddDecorator<While>()
				.AddLeaf<SuccessBehavior>().Complete()
			.Complete();
			TreeRoot treeRoot = builder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<While>()
				.AddLeaf<FailureBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<While>()
				.AddLeaf<RunningBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<While>()
				.AddLeaf<ErrorBehavior>().Complete()
			.Complete();
			treeRoot = builder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}
	}
}
