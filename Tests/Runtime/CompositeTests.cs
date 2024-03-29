// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Reflection;
using NUnit.Framework;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.Core.Leaves.StatusBehaviors;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Tests
{
	public static class CompositeTests
	{
		[Test]
		public static void ActiveSelectorTest()
		{
			const string currentIndexFieldName = "m_currentChildIndex";
			const string rootFieldName = "m_rootBehavior";
			const string childrenFieldName = "children";

			FieldInfo currentIndexField = typeof(ActiveSelector).GetField(currentIndexFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childrenField = typeof(Composite).GetField(childrenFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var propertyNames = new[]
			{
				new BlackboardPropertyName("value0"),
				new BlackboardPropertyName("value1"),
				new BlackboardPropertyName("value2"),
				new BlackboardPropertyName("value3"),
				new BlackboardPropertyName("value4")
			};

			var blackboard = new Blackboard();
			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<ActiveSelector>()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[2]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[3]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[4]).Complete()
			.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(0, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(0, currentIndexField.GetValue(root));
			for (int i = 1, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(3, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				if (i != 3)
				{
					Assert.AreNotEqual(Status.Running, children[i].status);
				}
			}

			blackboard.SetStructValue(propertyNames[1], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));
			Assert.AreEqual(Status.Abort, children[3].status);
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				if (i != 1)
				{
					Assert.AreNotEqual(Status.Running, children[i].status);
				}
			}

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(4, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(3, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			treeRoot.Dispose();
		}

		[Test]
		public static void ConcurrentTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childrenFieldName = "children";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childrenField = typeof(Composite).GetField(childrenFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var properties = new BlackboardPropertyName[]
			{
				new BlackboardPropertyName("value0"),
				new BlackboardPropertyName("value1"),
				new BlackboardPropertyName("value2")
			};

			var blackboard = new Blackboard();
			blackboard.SetStructValue(properties[0], Status.Success);
			blackboard.SetStructValue(properties[1], Status.Success);
			blackboard.SetStructValue(properties[2], Status.Success);

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Concurrent, Concurrent.Mode, Concurrent.Mode>(Concurrent.Mode.All, Concurrent.Mode.All)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[2]).Complete()
			.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[1], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(properties[1], Status.Failure);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(properties[2], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[0], Status.Failure);
			blackboard.SetStructValue(properties[2], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[2], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(properties[1], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Concurrent, Concurrent.Mode, Concurrent.Mode>(Concurrent.Mode.Any, Concurrent.Mode.Any)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[2]).Complete()
			.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			root = rootField.GetValue(treeRoot);
			children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(properties[0], Status.Success);
			blackboard.SetStructValue(properties[1], Status.Success);
			blackboard.SetStructValue(properties[2], Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[0], Status.Running);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[1], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[2], Status.Running);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[1], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(properties[2], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[1], Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Concurrent, Concurrent.Mode, Concurrent.Mode>(Concurrent.Mode.All, Concurrent.Mode.Any)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[2]).Complete()
				.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			root = rootField.GetValue(treeRoot);
			children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(properties[0], Status.Success);
			blackboard.SetStructValue(properties[1], Status.Success);
			blackboard.SetStructValue(properties[2], Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[1], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[2], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Concurrent, Concurrent.Mode, Concurrent.Mode>(Concurrent.Mode.Any, Concurrent.Mode.All)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(properties[2]).Complete()
				.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			root = rootField.GetValue(treeRoot);
			children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			blackboard.SetStructValue(properties[0], Status.Failure);
			blackboard.SetStructValue(properties[1], Status.Failure);
			blackboard.SetStructValue(properties[2], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[1], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(properties[2], Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0; i < 3; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			treeRoot.Dispose();
		}

		[Test]
		public static void ParallelTest()
		{
			const string rootFieldName = "m_rootBehavior";
			const string childrenFieldName = "children";

			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo childrenField = typeof(Composite).GetField(childrenFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var propertyNames = new[]
			{
				new BlackboardPropertyName("value0"),
				new BlackboardPropertyName("value1"),
				new BlackboardPropertyName("value2"),
				new BlackboardPropertyName("value3"),
				new BlackboardPropertyName("value4")
			};

			var blackboard = new Blackboard();
			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Parallel, Parallel.Mode>(Parallel.Mode.All)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[2]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[3]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[4]).Complete()
			.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			var children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Running);
			blackboard.SetStructValue(propertyNames[1], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			for (int i = 2, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[1], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Success);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			treeRoot.Dispose();

			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Parallel, Parallel.Mode>(Parallel.Mode.Any)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[2]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[3]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[4]).Complete()
			.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			root = rootField.GetValue(treeRoot);
			children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[1], Status.Running);
			blackboard.SetStructValue(propertyNames[4], Status.Running);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[4], Status.Error);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Running);
			blackboard.SetStructValue(propertyNames[1], Status.Running);
			blackboard.SetStructValue(propertyNames[2], Status.Running);
			blackboard.SetStructValue(propertyNames[3], Status.Running);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[4], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());

			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			treeRoot.Dispose();

			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Parallel, Parallel.Mode, Parallel.Mode>(Parallel.Mode.All, Parallel.Mode.Any)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[2]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[3]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[4]).Complete()
			.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			root = rootField.GetValue(treeRoot);
			children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[4], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Running);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[4], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			for (int i = 0, count = children.Length - 2; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Running);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			treeRoot.Dispose();

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);

			treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Parallel, Parallel.Mode, Parallel.Mode>(Parallel.Mode.Any, Parallel.Mode.All)
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[2]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[3]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[4]).Complete()
			.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			root = rootField.GetValue(treeRoot);
			children = (Behavior[])childrenField.GetValue(root);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[4], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Running);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[4], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			for (int i = 0, count = children.Length - 2; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Running);
			blackboard.SetStructValue(propertyNames[4], Status.Success);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i]);
			}

			treeRoot.Dispose();

			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Running);
			blackboard.SetStructValue(propertyNames[2], Status.Running);
			blackboard.SetStructValue(propertyNames[3], Status.Running);
			blackboard.SetStructValue(propertyNames[4], Status.Running);

			treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Parallel, Parallel.Mode>(Parallel.Mode.All)
				.AddLeaf<SetStructValue<int>, int, BlackboardPropertyName>(100, propertyNames[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[2]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[3]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[4]).Complete()
			.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(propertyNames[0], out int value));
			Assert.AreEqual(100, value);
			blackboard.SetStructValue(propertyNames[0], 0);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(propertyNames[0], out value));
			Assert.AreNotEqual(100, value);

			treeRoot.Dispose();
		}

		[Test]
		public static void SelectorTest()
		{
			const string currentIndexFieldName = "m_currentChildIndex";
			const string rootFieldName = "m_rootBehavior";

			FieldInfo currentIndexField = typeof(Selector).GetField(currentIndexFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var propertyNames = new[]
			{
				new BlackboardPropertyName("value0"),
				new BlackboardPropertyName("value1"),
				new BlackboardPropertyName("value2"),
				new BlackboardPropertyName("value3"),
				new BlackboardPropertyName("value4")
			};

			var blackboard = new Blackboard();
			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Selector>()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[2]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[3]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[4]).Complete()
			.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(0, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[1], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(4, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[3], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(3, currentIndexField.GetValue(root));

			treeRoot.Dispose();
		}

		[Test]
		public static void SequenceTest()
		{
			const string currentIndexFieldName = "m_currentChildIndex";
			const string rootFieldName = "m_rootBehavior";

			FieldInfo currentIndexField = typeof(Sequence).GetField(currentIndexFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo rootField = typeof(TreeRoot).GetField(rootFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);

			var propertyNames = new[]
			{
				new BlackboardPropertyName("value0"),
				new BlackboardPropertyName("value1"),
				new BlackboardPropertyName("value2"),
				new BlackboardPropertyName("value3"),
				new BlackboardPropertyName("value4")
			};

			var blackboard = new Blackboard();
			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddComposite<Sequence>()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[0]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[1]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[2]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[3]).Complete()
				.AddLeaf<VariableBehavior, BlackboardPropertyName>(propertyNames[4]).Complete()
			.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(treeRoot);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(4, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[1], Status.Running);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[1], Status.Success);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(2, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[2], Status.Error);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(2, currentIndexField.GetValue(root));

			treeRoot.Dispose();
		}
	}
}
