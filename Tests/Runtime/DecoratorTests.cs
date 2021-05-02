// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using NUnit.Framework;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.Leaves.StatusBehaviors;

namespace Zor.BehaviorTree.Tests
{
	public static class DecoratorTests
	{
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

		[Test]
		public static void RepeatTest()
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
