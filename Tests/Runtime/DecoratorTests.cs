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
				.AddLeaf<SuccessBehavior>().Finish()
			.Finish();
			TreeRoot treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Inverter>()
				.AddLeaf<RunningBehavior>().Finish()
			.Finish();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Inverter>()
				.AddLeaf<FailureBehavior>().Finish()
			.Finish();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddDecorator<Inverter>()
				.AddLeaf<ErrorBehavior>().Finish()
			.Finish();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			treeRoot.Dispose();
		}

		[Test]
		public static void RepeatTests()
		{
			var builder = new TreeBuilder();
			builder.AddDecorator<Repeater, uint>(3u)
				.AddLeaf<SuccessBehavior>().Finish()
			.Finish();
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
				.AddLeaf<RunningBehavior>().Finish()
			.Finish();
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
				.AddLeaf<FailureBehavior>().Finish()
			.Finish();
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
				.AddLeaf<ErrorBehavior>().Finish()
			.Finish();
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
	}
}
