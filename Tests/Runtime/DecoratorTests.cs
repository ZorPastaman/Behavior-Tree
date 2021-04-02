// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using NUnit.Framework;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.StatusBehaviors;

namespace Zor.BehaviorTree.Tests
{
	public static class DecoratorTests
	{
		[Test]
		public static void InverterTest()
		{
			var builder = new TreeBuilder();
			builder.AddBehavior<Inverter>()
				.AddBehavior<SuccessBehavior>().Finish()
			.Finish();
			TreeRoot treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddBehavior<Inverter>()
				.AddBehavior<RunningBehavior>().Finish()
			.Finish();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			Assert.AreEqual(Status.Running, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddBehavior<Inverter>()
				.AddBehavior<FailureBehavior>().Finish()
			.Finish();
			treeRoot = builder.Build();
			treeRoot.Initialize();
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			treeRoot.Dispose();

			builder = new TreeBuilder();
			builder.AddBehavior<Inverter>()
				.AddBehavior<ErrorBehavior>().Finish()
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
			builder.AddBehavior<Repeat>(3u)
				.AddBehavior<SuccessBehavior>().Finish()
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
			builder.AddBehavior<Repeat>(3u)
				.AddBehavior<RunningBehavior>().Finish()
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
			builder.AddBehavior<Repeat>(3u)
				.AddBehavior<FailureBehavior>().Finish()
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
			builder.AddBehavior<Repeat>(3u)
				.AddBehavior<ErrorBehavior>().Finish()
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
