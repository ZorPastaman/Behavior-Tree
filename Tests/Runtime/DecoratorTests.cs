// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using NUnit.Framework;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.StatusBehaviors;

namespace Zor.BehaviorTree.Tests.Runtime
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
			Tree tree = builder.Build();
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(Status.Failure, tree.Tick());

			builder = new TreeBuilder();
			builder.AddBehavior<Inverter>()
				.AddBehavior<RunningBehavior>().Finish()
			.Finish();
			tree = builder.Build();
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());

			builder = new TreeBuilder();
			builder.AddBehavior<Inverter>()
				.AddBehavior<FailureBehavior>().Finish()
			.Finish();
			tree = builder.Build();
			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.AreEqual(Status.Success, tree.Tick());

			builder = new TreeBuilder();
			builder.AddBehavior<Inverter>()
				.AddBehavior<ErrorBehavior>().Finish()
			.Finish();
			tree = builder.Build();
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(Status.Error, tree.Tick());
		}

		[Test]
		public static void RepeatTests()
		{
			var builder = new TreeBuilder();
			builder.AddBehavior<Repeat>(3u)
				.AddBehavior<SuccessBehavior>().Finish()
			.Finish();
			Tree tree = builder.Build();
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Success, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Success, tree.Tick());

			builder = new TreeBuilder();
			builder.AddBehavior<Repeat>(3u)
				.AddBehavior<RunningBehavior>().Finish()
			.Finish();
			tree = builder.Build();
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());
			Assert.AreEqual(Status.Running, tree.Tick());

			builder = new TreeBuilder();
			builder.AddBehavior<Repeat>(3u)
				.AddBehavior<FailureBehavior>().Finish()
			.Finish();
			tree = builder.Build();
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(Status.Failure, tree.Tick());
			Assert.AreEqual(Status.Failure, tree.Tick());

			builder = new TreeBuilder();
			builder.AddBehavior<Repeat>(3u)
				.AddBehavior<ErrorBehavior>().Finish()
			.Finish();
			tree = builder.Build();
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(Status.Error, tree.Tick());
			Assert.AreEqual(Status.Error, tree.Tick());
		}
	}
}
