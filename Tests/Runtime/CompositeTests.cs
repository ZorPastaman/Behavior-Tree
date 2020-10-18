// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Reflection;
using NUnit.Framework;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.StatusBehaviors;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Tests
{
	public static class CompositeTests
	{
		[Test]
		public static void ActiveSelectorTest()
		{
			const string currentIndexFieldName = "m_currentChildIndex";
			const string rootFieldName = "m_root";
			const string childrenFieldName = "children";

			FieldInfo currentIndexField = typeof(ActiveSelector).GetField(currentIndexFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo rootField = typeof(Tree).GetField(rootFieldName,
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
			treeBuilder.AddBehavior<ActiveSelector>()
				.AddBehavior<VariableBehavior>(propertyNames[0]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[1]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[2]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[3]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[4]).Finish()
			.Finish();
			Tree tree = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(tree);
			var children = (Behavior[])childrenField.GetValue(root);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.AreEqual(0, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Running);
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(0, currentIndexField.GetValue(root));
			for (int i = 1, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Running);
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(3, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				if (i != 3)
				{
					Assert.AreNotEqual(Status.Running, children[i].status);
				}
			}

			blackboard.SetStructValue(propertyNames[1], Status.Running);
			Assert.AreEqual(Status.Running, tree.Tick());
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
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(5, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Error);
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(3, currentIndexField.GetValue(root));
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			tree.Dispose();
		}

		[Test]
		public static void ParallelTest()
		{
			const string rootFieldName = "m_root";
			const string childrenFieldName = "children";

			FieldInfo rootField = typeof(Tree).GetField(rootFieldName,
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
			treeBuilder.AddBehavior<Parallel>(Parallel.Mode.All)
				.AddBehavior<VariableBehavior>(propertyNames[0]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[1]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[2]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[3]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[4]).Finish()
			.Finish();
			Tree tree = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(tree);
			var children = (Behavior[])childrenField.GetValue(root);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Running);
			Assert.AreEqual(Status.Running, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				if (i != 3)
				{
					Assert.AreNotEqual(Status.Running, children[i].status);
				}
			}

			blackboard.SetStructValue(propertyNames[4], Status.Error);
			Assert.AreEqual(Status.Error, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Running, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				if (i != 3)
				{
					Assert.AreNotEqual(Status.Running, children[i].status);
				}
			}

			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			Assert.AreEqual(Status.Running, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			Assert.AreEqual(Status.Failure, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			tree.Dispose();

			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<Parallel>(Parallel.Mode.Any)
				.AddBehavior<VariableBehavior>(propertyNames[0]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[1]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[2]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[3]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[4]).Finish()
			.Finish();
			tree = treeBuilder.Build(blackboard);
			root = rootField.GetValue(tree);
			children = (Behavior[])childrenField.GetValue(root);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Running);
			Assert.AreEqual(Status.Success, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			Assert.AreEqual(Status.Success, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			Assert.AreEqual(Status.Failure, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[1], Status.Error);
			Assert.AreEqual(Status.Error, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			tree.Dispose();

			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<Parallel>(Parallel.Mode.All, Parallel.Mode.Any)
				.AddBehavior<VariableBehavior>(propertyNames[0]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[1]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[2]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[3]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[4]).Finish()
			.Finish();
			tree = treeBuilder.Build(blackboard);
			root = rootField.GetValue(tree);
			children = (Behavior[])childrenField.GetValue(root);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Running);
			Assert.AreEqual(Status.Running, tree.Tick());
			for (int i = 1, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			Assert.AreEqual(Status.Failure, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[3], Status.Error);
			Assert.AreEqual(Status.Error, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			tree.Dispose();

			blackboard.SetStructValue(propertyNames[0], Status.Success);
			blackboard.SetStructValue(propertyNames[1], Status.Success);
			blackboard.SetStructValue(propertyNames[2], Status.Success);
			blackboard.SetStructValue(propertyNames[3], Status.Success);
			blackboard.SetStructValue(propertyNames[4], Status.Success);

			treeBuilder = new TreeBuilder();
			treeBuilder.AddBehavior<Parallel>(Parallel.Mode.Any, Parallel.Mode.All)
				.AddBehavior<VariableBehavior>(propertyNames[0]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[1]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[2]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[3]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[4]).Finish()
			.Finish();
			tree = treeBuilder.Build(blackboard);
			root = rootField.GetValue(tree);
			children = (Behavior[])childrenField.GetValue(root);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[4], Status.Running);
			Assert.AreEqual(Status.Success, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Error);
			Assert.AreEqual(Status.Error, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Running);
			Assert.AreEqual(Status.Success, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[1], Status.Running);
			blackboard.SetStructValue(propertyNames[2], Status.Running);
			blackboard.SetStructValue(propertyNames[3], Status.Running);
			Assert.AreEqual(Status.Running, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreEqual(Status.Running, children[i].status);
			}

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Failure, tree.Tick());
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				Assert.AreNotEqual(Status.Running, children[i].status);
			}

			tree.Dispose();
		}

		[Test]
		public static void SelectorTest()
		{
			const string currentIndexFieldName = "m_currentChildIndex";
			const string rootFieldName = "m_root";

			FieldInfo currentIndexField = typeof(Selector).GetField(currentIndexFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo rootField = typeof(Tree).GetField(rootFieldName,
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
			treeBuilder.AddBehavior<Selector>()
				.AddBehavior<VariableBehavior>(propertyNames[0]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[1]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[2]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[3]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[4]).Finish()
			.Finish();
			Tree tree = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(tree);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.AreEqual(0, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[0], Status.Failure);
			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[1], Status.Running);
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[1], Status.Failure);
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(5, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[3], Status.Error);
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(3, currentIndexField.GetValue(root));

			tree.Dispose();
		}

		[Test]
		public static void SequenceTest()
		{
			const string currentIndexFieldName = "m_currentChildIndex";
			const string rootFieldName = "m_root";

			FieldInfo currentIndexField = typeof(Sequence).GetField(currentIndexFieldName,
				BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfo rootField = typeof(Tree).GetField(rootFieldName,
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
			treeBuilder.AddBehavior<Sequence>()
				.AddBehavior<VariableBehavior>(propertyNames[0]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[1]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[2]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[3]).Finish()
				.AddBehavior<VariableBehavior>(propertyNames[4]).Finish()
			.Finish();
			Tree tree = treeBuilder.Build(blackboard);
			object root = rootField.GetValue(tree);
			tree.Initialize();

			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.AreEqual(5, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[1], Status.Running);
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[2], Status.Failure);
			blackboard.SetStructValue(propertyNames[3], Status.Failure);
			blackboard.SetStructValue(propertyNames[4], Status.Failure);
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(1, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[1], Status.Success);
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(2, currentIndexField.GetValue(root));

			blackboard.SetStructValue(propertyNames[2], Status.Error);
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(2, currentIndexField.GetValue(root));

			tree.Dispose();
		}
	}
}
