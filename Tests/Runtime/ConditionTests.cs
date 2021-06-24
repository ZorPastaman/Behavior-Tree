// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using NUnit.Framework;
using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Tests
{
	public static class ConditionTests
	{
		[Test]
		public static void BoundsContainsTest()
		{
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var pointPropertyName = new BlackboardPropertyName("point");
			var bounds = new Bounds(Vector3.zero, Vector3.one);
			Vector3 point = Vector3.zero;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<BoundsContains, BlackboardPropertyName, BlackboardPropertyName>(boundsPropertyName,
				pointPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Bounds>(boundsPropertyName);
			blackboard.SetStructValue(pointPropertyName, point);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(pointPropertyName, new Vector3(5f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void BoundsIntersectRayTest()
		{
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var rayPropertyName = new BlackboardPropertyName("ray");
			var bounds = new Bounds(Vector3.zero, Vector3.one);
			var ray = new Ray(new Vector3(2f, 0f, 0f), new Vector3(-5f, 0f, 0f));
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<BoundsIntersectRay, BlackboardPropertyName, BlackboardPropertyName>(boundsPropertyName,
				rayPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Bounds>(boundsPropertyName);
			blackboard.SetStructValue(rayPropertyName, ray);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(rayPropertyName, new Ray(new Vector3(2f, 0f, 0f), new Vector3(5f, 0f, 0f)));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void BoundsIntersectsTest()
		{
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var otherPropertyName = new BlackboardPropertyName("other");
			var bounds = new Bounds(Vector3.zero, Vector3.one);
			var other = new Bounds(new Vector3(0.5f, 0f, 0f), Vector3.one);
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<BoundsIntersects, BlackboardPropertyName, BlackboardPropertyName>(boundsPropertyName,
				otherPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Bounds>(boundsPropertyName);
			blackboard.SetStructValue(otherPropertyName, other);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(otherPropertyName, new Bounds(new Vector3(5f, 0f, 0f), Vector3.one));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

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
		public static void IsBehaviourEnabledTest()
		{
			var property = new BlackboardPropertyName("behaviour");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsBehaviourEnabled, BlackboardPropertyName>(property).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Behaviour>(property, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Behaviour behaviour = new GameObject().AddComponent<Camera>();
			behaviour.enabled = false;
			blackboard.SetClassValue(property, behaviour);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			behaviour.enabled = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsBoundsDistanceGreaterTest()
		{
			var bounds = new Bounds(Vector3.zero, Vector3.one);
			var point = new Vector3(5f, 0f, 0f);
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var pointPropertyName = new BlackboardPropertyName("point");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsBoundsDistanceGreater, BlackboardPropertyName, BlackboardPropertyName, float>(
				boundsPropertyName, pointPropertyName, 3f).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Bounds>(boundsPropertyName);
			blackboard.SetStructValue(pointPropertyName, point);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(pointPropertyName, new Vector3(2f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsBoundsDistanceLessTest()
		{
			var bounds = new Bounds(Vector3.zero, Vector3.one);
			var point = new Vector3(5f, 0f, 0f);
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var pointPropertyName = new BlackboardPropertyName("point");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsBoundsDistanceLess, BlackboardPropertyName, BlackboardPropertyName, float>(
				boundsPropertyName, pointPropertyName, 3f).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Bounds>(boundsPropertyName);
			blackboard.SetStructValue(pointPropertyName, point);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(boundsPropertyName, bounds);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(pointPropertyName, new Vector3(2f, 0f, 0f));
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
		public static void IsClassEqualVariableTest()
		{
			var firstPropertyName = new BlackboardPropertyName("first");
			var secondPropertyName = new BlackboardPropertyName("second");
			const string firstValue = "firstValue";
			const string secondValue = "secondValue";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueEqualVariable<string>, BlackboardPropertyName, BlackboardPropertyName>(firstPropertyName, secondPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveObject<string>(firstPropertyName);
			blackboard.SetClassValue(secondPropertyName, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(secondPropertyName, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetClassValue<string>(secondPropertyName, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue<string>(firstPropertyName, null);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

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

			value = null;
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.RemoveObject<string>(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsClassGreaterVariableTest()
		{
			var firstPropertyName = new BlackboardPropertyName("first");
			var secondPropertyName = new BlackboardPropertyName("second");
			const string firstValue = "firstValue";
			const string secondValue = "secondValue";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueGreaterVariable<string>, BlackboardPropertyName, BlackboardPropertyName>(firstPropertyName, secondPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveObject<string>(firstPropertyName);
			blackboard.SetClassValue(secondPropertyName, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(secondPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, secondValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetClassValue<string>(firstPropertyName, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue<string>(secondPropertyName, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

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

			value = null;
			blackboard.SetClassValue(propertyName, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.RemoveObject<string>(propertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsClassLessVariableTest()
		{
			var firstPropertyName = new BlackboardPropertyName("first");
			var secondPropertyName = new BlackboardPropertyName("second");
			const string firstValue = "firstValue";
			const string secondValue = "secondValue";
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsClassValueLessVariable<string>, BlackboardPropertyName, BlackboardPropertyName>(firstPropertyName, secondPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveObject<string>(firstPropertyName);
			blackboard.SetClassValue(secondPropertyName, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetClassValue(secondPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, secondValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue<string>(firstPropertyName, null);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetClassValue<string>(secondPropertyName, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetClassValue(firstPropertyName, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColliderEnabledTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var blackboard = new Blackboard();
			var collider = new GameObject().AddComponent<SphereCollider>();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColliderEnabled, BlackboardPropertyName>(colliderPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			collider.enabled = false;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColliderRaycastTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var originPropertyName = new BlackboardPropertyName("origin");
			var directionPropertyName = new BlackboardPropertyName("direction");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColliderRaycast, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					colliderPropertyName, originPropertyName, directionPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var collider = new GameObject().AddComponent<SphereCollider>();
			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var origin = new Vector3(10f, 0f, 0f);
			blackboard.SetStructValue(originPropertyName, origin);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Vector3>(originPropertyName);
			var direction = new Vector3(-20f, 0f, 0f);
			blackboard.SetStructValue(directionPropertyName, direction);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveObject<Collider>(colliderPropertyName);
			blackboard.SetStructValue(originPropertyName, origin);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(colliderPropertyName, collider);
			blackboard.RemoveStruct<Vector3>(originPropertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originPropertyName, origin);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			direction.x = -direction.x;
			blackboard.SetStructValue(directionPropertyName, direction);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColliderTriggerTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColliderTrigger, BlackboardPropertyName>(colliderPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var collider = new GameObject().AddComponent<SphereCollider>();
			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			collider.isTrigger = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());

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
		public static void IsStructEqualVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("firstProperty");
			var secondProperty = new BlackboardPropertyName("secondProperty");
			const int firstValue = 0;
			const int secondValue = 1;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsStructValueEqualVariable<int>, BlackboardPropertyName, BlackboardPropertyName>(firstProperty, secondProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveStruct<int>(firstProperty);
			blackboard.SetStructValue(secondProperty, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(secondProperty, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, secondValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

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
		public static void IsStructGreaterVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("firstProperty");
			var secondProperty = new BlackboardPropertyName("secondProperty");
			const int firstValue = 0;
			const int secondValue = 1;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsStructValueGreaterVariable<int>, BlackboardPropertyName, BlackboardPropertyName>(firstProperty, secondProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveStruct<int>(firstProperty);
			blackboard.SetStructValue(secondProperty, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(secondProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, secondValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

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

		[Test]
		public static void IsStructLessVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("firstProperty");
			var secondProperty = new BlackboardPropertyName("secondProperty");
			const int firstValue = 0;
			const int secondValue = 1;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsStructValueLessVariable<int>, BlackboardPropertyName, BlackboardPropertyName>(firstProperty, secondProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveStruct<int>(firstProperty);
			blackboard.SetStructValue(secondProperty, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(secondProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, secondValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
	}
}
