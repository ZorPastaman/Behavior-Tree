// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using NUnit.Framework;
using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.Tests
{
	public static class StateToStatusTests
	{
		[Test]
		public static void FinishedToStatusTest()
		{
			Assert.AreEqual(Status.Success, StateToStatusHelper.FinishedToStatus(true));
			Assert.AreEqual(Status.Running, StateToStatusHelper.FinishedToStatus(false));

			Assert.AreEqual(Status.Success, StateToStatusHelper.FinishedToStatus(true, true));
			Assert.AreEqual(Status.Running, StateToStatusHelper.FinishedToStatus(false, true));

			Assert.AreEqual(Status.Error, StateToStatusHelper.FinishedToStatus(true, false));
			Assert.AreEqual(Status.Error, StateToStatusHelper.FinishedToStatus(false, false));
		}

		[Test]
		public static void FailedToStatusTest()
		{
			Assert.AreEqual(Status.Failure, StateToStatusHelper.FailedToStatus(true));
			Assert.AreEqual(Status.Running, StateToStatusHelper.FailedToStatus(false));

			Assert.AreEqual(Status.Failure, StateToStatusHelper.FailedToStatus(true, true));
			Assert.AreEqual(Status.Running, StateToStatusHelper.FailedToStatus(false, true));

			Assert.AreEqual(Status.Error, StateToStatusHelper.FailedToStatus(true, false));
			Assert.AreEqual(Status.Error, StateToStatusHelper.FailedToStatus(false, false));
		}

		[Test]
		public static void FinishedFailedToStatusTest()
		{
			Assert.AreEqual(Status.Success, StateToStatusHelper.FinishedFailedToStatus(true, false));
			Assert.AreEqual(Status.Failure, StateToStatusHelper.FinishedFailedToStatus(false, true));
			Assert.AreEqual(Status.Running, StateToStatusHelper.FinishedFailedToStatus(false, false));
			Assert.AreEqual(Status.Error, StateToStatusHelper.FinishedFailedToStatus(true, true));

			Assert.AreEqual(Status.Success, StateToStatusHelper.FinishedFailedToStatus(true, false, true));
			Assert.AreEqual(Status.Failure, StateToStatusHelper.FinishedFailedToStatus(false, true, true));
			Assert.AreEqual(Status.Running, StateToStatusHelper.FinishedFailedToStatus(false, false, true));
			Assert.AreEqual(Status.Error, StateToStatusHelper.FinishedFailedToStatus(true, true, true));

			Assert.AreEqual(Status.Error, StateToStatusHelper.FinishedFailedToStatus(true, false, false));
			Assert.AreEqual(Status.Error, StateToStatusHelper.FinishedFailedToStatus(false, true, false));
			Assert.AreEqual(Status.Error, StateToStatusHelper.FinishedFailedToStatus(false, false, false));
			Assert.AreEqual(Status.Error, StateToStatusHelper.FinishedFailedToStatus(true, true, false));
		}

		[Test]
		public static void ConditionToStatusTest()
		{
			Assert.AreEqual(Status.Success, StateToStatusHelper.ConditionToStatus(true));
			Assert.AreEqual(Status.Failure, StateToStatusHelper.ConditionToStatus(false));

			Assert.AreEqual(Status.Success, StateToStatusHelper.ConditionToStatus(true, true));
			Assert.AreEqual(Status.Failure, StateToStatusHelper.ConditionToStatus(false, true));

			Assert.AreEqual(Status.Error, StateToStatusHelper.ConditionToStatus(true, false));
			Assert.AreEqual(Status.Error, StateToStatusHelper.ConditionToStatus(false, false));

			Assert.AreEqual(Status.Failure,
				StateToStatusHelper.ConditionToStatus(false, Status.Failure, Status.Success));
			Assert.AreEqual(Status.Success,
				StateToStatusHelper.ConditionToStatus(true, Status.Failure, Status.Success));

			Assert.AreEqual(Status.Running,
				StateToStatusHelper.ConditionToStatus(false, Status.Running, Status.Success));
			Assert.AreEqual(Status.Success,
				StateToStatusHelper.ConditionToStatus(true, Status.Running, Status.Success));

			Assert.AreEqual(Status.Error,
				StateToStatusHelper.ConditionToStatus(false, Status.Error, Status.Failure));
			Assert.AreEqual(Status.Failure,
				StateToStatusHelper.ConditionToStatus(true, Status.Error, Status.Failure));
		}
	}
}
