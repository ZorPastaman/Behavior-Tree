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
		public static void GetBoundsClosestPointTest()
		{
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var pointPropertyName = new BlackboardPropertyName("point");
			var closestPointPropertyName = new BlackboardPropertyName("closest");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetBoundsClosestPoint, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					boundsPropertyName, pointPropertyName, closestPointPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.SetStructValue(boundsPropertyName, new Bounds());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.RemoveStruct<Bounds>(boundsPropertyName);
			blackboard.SetStructValue(pointPropertyName, new Vector3());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.SetStructValue(boundsPropertyName, new Bounds(Vector3.zero, Vector3.one));
			blackboard.SetStructValue(pointPropertyName, new Vector3(2f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(closestPointPropertyName, out Vector3 closestPoint));
			Assert.AreEqual(new Vector3(0.5f, 0f, 0f), closestPoint);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetBoundsIntersectRayDistanceTest()
		{
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var rayPropertyName = new BlackboardPropertyName("ray");
			var distancePropertyName = new BlackboardPropertyName("distance");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetBoundsIntersectRayDistance, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName>(boundsPropertyName, rayPropertyName, distancePropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<float>(distancePropertyName));

			blackboard.SetStructValue(boundsPropertyName, new Bounds());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<float>(distancePropertyName));

			blackboard.RemoveStruct<Bounds>(boundsPropertyName);
			blackboard.SetStructValue(rayPropertyName, new Ray());
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<float>(distancePropertyName));

			blackboard.SetStructValue(boundsPropertyName, new Bounds(Vector3.zero, Vector3.one));
			blackboard.SetStructValue(rayPropertyName, new Ray(new Vector3(2f, 0f, 0f), new Vector3(-5f, 0f, 0f)));
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(distancePropertyName, out float distance));
			Assert.AreEqual(1.5f, distance, 0.0001f);

			blackboard.RemoveStruct<float>(distancePropertyName);
			blackboard.SetStructValue(rayPropertyName, new Ray(new Vector3(2f, 0f, 0f), new Vector3(5f, 0f, 0f)));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<float>(distancePropertyName));

			treeRoot.Dispose();
		}

		[Test]
		public static void GetColliderBoundsTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var blackboard = new Blackboard();
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 5f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<GetColliderBounds, BlackboardPropertyName, BlackboardPropertyName>(colliderPropertyName,
				boundsPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Bounds>(boundsPropertyName));

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Bounds>(boundsPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(boundsPropertyName, out Bounds bounds));
			Assert.AreEqual(collider.bounds, bounds);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetColliderClosestPointTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var pointPropertyName = new BlackboardPropertyName("point");
			var closestPointPropertyName = new BlackboardPropertyName("closestPoint");
			var blackboard = new Blackboard();
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 5f;
			var point = new Vector3(10f, 10f, 0f);

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetColliderClosestPoint, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName>(colliderPropertyName, pointPropertyName, closestPointPropertyName)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.RemoveObject<Collider>(colliderPropertyName);
			blackboard.SetStructValue(pointPropertyName, point);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(closestPointPropertyName, out Vector3 closestPoint));
			Assert.AreEqual(collider.ClosestPoint(point), closestPoint);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetColliderClosestPointOnBoundsTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var pointPropertyName = new BlackboardPropertyName("point");
			var closestPointPropertyName = new BlackboardPropertyName("closestPoint");
			var blackboard = new Blackboard();
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 5f;
			var point = new Vector3(10f, 10f, 0f);

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetColliderClosestPointOnBounds, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName>(colliderPropertyName, pointPropertyName, closestPointPropertyName)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.RemoveObject<Collider>(colliderPropertyName);
			blackboard.SetStructValue(pointPropertyName, point);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector3>(closestPointPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(closestPointPropertyName, out Vector3 closestPoint));
			Assert.AreEqual(collider.ClosestPointOnBounds(point), closestPoint);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetColliderEnabledTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var enabledPropertyName = new BlackboardPropertyName("enabled");
			var blackboard = new Blackboard();
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 5f;
			collider.enabled = false;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetColliderEnabled, BlackboardPropertyName, BlackboardPropertyName>(colliderPropertyName,
					enabledPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(enabledPropertyName));

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(enabledPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(enabledPropertyName, out bool enabled));
			Assert.AreEqual(collider.enabled, enabled);

			collider.enabled = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(enabledPropertyName, out enabled));
			Assert.AreEqual(collider.enabled, enabled);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetColliderRigidbodyTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var rigidbodyPropertyName = new BlackboardPropertyName("rigidbody");
			var blackboard = new Blackboard();
			var go = new GameObject();
			var collider = go.AddComponent<SphereCollider>();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<GetColliderRigidbody, BlackboardPropertyName, BlackboardPropertyName>(
				colliderPropertyName, rigidbodyPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Rigidbody>(rigidbodyPropertyName));

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Rigidbody>(rigidbodyPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(rigidbodyPropertyName, out Rigidbody rigid));
			Assert.AreEqual(collider.attachedRigidbody, rigid);
			Assert.AreEqual(null, rigid);

			var rigidbody = go.AddComponent<Rigidbody>();
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(rigidbodyPropertyName, out rigid));
			Assert.AreEqual(collider.attachedRigidbody, rigid);
			Assert.AreEqual(rigidbody, rigid);

			treeRoot.Dispose();
		}

		[Test]
		public static void RaycastColliderTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var originPropertyName = new BlackboardPropertyName("origin");
			var directionPropertyName = new BlackboardPropertyName("direction");
			var hitPropertyName = new BlackboardPropertyName("hit");
			var blackboard = new Blackboard();

			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 5f;
			var origin = new Vector3(10f, 0f, 0f);
			var direction = new Vector3(-20f, 0f, 0f);

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<RaycastCollider, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName>(colliderPropertyName, originPropertyName, directionPropertyName,
					hitPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.SetStructValue(originPropertyName, origin);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.RemoveStruct<Vector3>(originPropertyName);
			blackboard.SetStructValue(directionPropertyName, direction);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.RemoveObject<Collider>(colliderPropertyName);
			blackboard.RemoveStruct<Vector3>(directionPropertyName);
			blackboard.SetStructValue(originPropertyName, origin);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.RemoveObject<Collider>(colliderPropertyName);
			blackboard.SetStructValue(directionPropertyName, direction);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.RemoveStruct<Vector3>(originPropertyName);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.RemoveObject<Collider>(colliderPropertyName);
			blackboard.SetStructValue(originPropertyName, origin);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(hitPropertyName, out RaycastHit hit));
			collider.Raycast(new Ray(origin, direction), out RaycastHit hitInfo, (direction - origin).magnitude);
			Assert.AreEqual(hitInfo, hit);

			blackboard.RemoveStruct<RaycastHit>(hitPropertyName);
			blackboard.SetStructValue(directionPropertyName, new Vector3(20f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<RaycastHit>(hitPropertyName));

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
		public static void SetBoundsTest()
		{
			Vector3 center = Vector3.zero;
			Vector3 size = Vector3.one;
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetBounds, Vector3, Vector3, BlackboardPropertyName>(center, size, boundsPropertyName)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(boundsPropertyName, out Bounds bounds));
			Assert.AreEqual(new Bounds(center, size), bounds);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetBoundsVariableTest()
		{
			Vector3 center = Vector3.zero;
			Vector3 size = Vector3.one;
			var centerPropertyName = new BlackboardPropertyName("center");
			var sizePropertyName = new BlackboardPropertyName("size");
			var boundsPropertyName = new BlackboardPropertyName("bounds");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetBoundsVariable, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					centerPropertyName, sizePropertyName, boundsPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Bounds>(boundsPropertyName));

			blackboard.SetStructValue(centerPropertyName, center);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Bounds>(boundsPropertyName));

			blackboard.RemoveStruct<Vector3>(centerPropertyName);
			blackboard.SetStructValue(sizePropertyName, size);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Bounds>(boundsPropertyName));

			blackboard.SetStructValue(centerPropertyName, center);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(boundsPropertyName, out Bounds bounds));
			Assert.AreEqual(new Bounds(center, size), bounds);

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
		public static void SetColliderEnabledTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetColliderEnabled, BlackboardPropertyName, bool>(colliderPropertyName, false)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var collider = new GameObject().AddComponent<SphereCollider>();
			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(false, collider.enabled);

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetColliderEnabled, BlackboardPropertyName, bool>(colliderPropertyName, true)
				.Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(true, collider.enabled);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetColliderEnabledVariableTest()
		{
			var colliderPropertyName = new BlackboardPropertyName("collider");
			var enabledPropertyName = new BlackboardPropertyName("enabled");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColliderEnabledVariable, BlackboardPropertyName, BlackboardPropertyName>(
					colliderPropertyName, enabledPropertyName).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<Collider>(colliderPropertyName, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var collider = new GameObject().AddComponent<SphereCollider>();
			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveObject<Collider>(colliderPropertyName);
			blackboard.SetStructValue(enabledPropertyName, false);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(colliderPropertyName, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(false, collider.enabled);

			blackboard.SetStructValue(enabledPropertyName, true);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(true, collider.enabled);

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
