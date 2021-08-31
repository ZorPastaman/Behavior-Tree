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
		public static void BoxCastTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var centerProperty = new BlackboardPropertyName("center");
			var halfExtentsProperty = new BlackboardPropertyName("half extents");
			var directionProperty = new BlackboardPropertyName("direction");
			var orientationProperty = new BlackboardPropertyName("orientation");
			const float maxDistance = 15f;
			LayerMask layerMask = 1;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<BoxCast, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName, float, LayerMask>(centerProperty, halfExtentsProperty,
				directionProperty, orientationProperty, maxDistance, layerMask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(centerProperty, new Vector3(25f, 0f, 0f));
			blackboard.SetStructValue(halfExtentsProperty, new Vector3(5f, 5f, 5f));
			blackboard.SetStructValue(directionProperty, new Vector3(-1f, 0f, 0f));
			blackboard.SetStructValue(orientationProperty, Quaternion.identity);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(directionProperty, new Vector3(1f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void BoxCastVariableTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var centerProperty = new BlackboardPropertyName("center");
			var halfExtentsProperty = new BlackboardPropertyName("half extents");
			var directionProperty = new BlackboardPropertyName("direction");
			var orientationProperty = new BlackboardPropertyName("orientation");
			var maxDistanceProperty = new BlackboardPropertyName("maxDistance");
			var layerMaskProperty = new BlackboardPropertyName("layerMask");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<BoxCastVariable, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(centerProperty,
				halfExtentsProperty, directionProperty, orientationProperty, maxDistanceProperty, layerMaskProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(centerProperty, new Vector3(25f, 0f, 0f));
			blackboard.SetStructValue(halfExtentsProperty, new Vector3(5f, 5f, 5f));
			blackboard.SetStructValue(directionProperty, new Vector3(-1f, 0f, 0f));
			blackboard.SetStructValue(orientationProperty, Quaternion.identity);
			blackboard.SetStructValue(maxDistanceProperty, 15f);
			blackboard.SetStructValue(layerMaskProperty, (LayerMask)1);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(directionProperty, new Vector3(1f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CapsuleCastTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var point1Property = new BlackboardPropertyName("point1");
			var point2Property = new BlackboardPropertyName("point2");
			var radiusProperty = new BlackboardPropertyName("radius");
			var directionProperty = new BlackboardPropertyName("direction");
			const float maxDistance = 15f;
			LayerMask layerMask = 1;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CapsuleCast, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName, BlackboardPropertyName, float, LayerMask>(point1Property, point2Property,
					radiusProperty, directionProperty, maxDistance, layerMask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(point1Property, new Vector3(20f, 5f, 0f));
			blackboard.SetStructValue(point2Property, new Vector3(20f, -5f, 0f));
			blackboard.SetStructValue(radiusProperty, 5f);
			blackboard.SetStructValue(directionProperty, new Vector3(-1f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(directionProperty, new Vector3(1f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CapsuleCastVariableTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var point1Property = new BlackboardPropertyName("point1");
			var point2Property = new BlackboardPropertyName("point2");
			var radiusProperty = new BlackboardPropertyName("radius");
			var directionProperty = new BlackboardPropertyName("direction");
			var maxDistanceProperty = new BlackboardPropertyName("maxDistance");
			var layerMaskProperty = new BlackboardPropertyName("layerMask");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CapsuleCastVariable, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
				point1Property, point2Property, radiusProperty, directionProperty, maxDistanceProperty,
				layerMaskProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(point1Property, new Vector3(20f, 5f, 0f));
			blackboard.SetStructValue(point2Property, new Vector3(20f, -5f, 0f));
			blackboard.SetStructValue(radiusProperty, 5f);
			blackboard.SetStructValue(directionProperty, new Vector3(-1f, 0f, 0f));
			blackboard.SetStructValue(maxDistanceProperty, 15f);
			blackboard.SetStructValue(layerMaskProperty, (LayerMask)1);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(directionProperty, new Vector3(1f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void LinecastTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var originProperty = new BlackboardPropertyName("origin");
			var endProperty = new BlackboardPropertyName("end");
			LayerMask layerMask = 1;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<Linecast, BlackboardPropertyName, BlackboardPropertyName, LayerMask>(originProperty,
				endProperty, layerMask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(15f, 0f, 0f));
			blackboard.SetStructValue(endProperty, new Vector3(-15f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(endProperty, new Vector3(20f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void LinecastVariableTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var originProperty = new BlackboardPropertyName("origin");
			var endProperty = new BlackboardPropertyName("end");
			var layerMaskProperty = new BlackboardPropertyName("layerMask");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<LinecastVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(originProperty, endProperty, layerMaskProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(15f, 0f, 0f));
			blackboard.SetStructValue(endProperty, new Vector3(-15f, 0f, 0f));
			blackboard.SetStructValue(layerMaskProperty, (LayerMask)1);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(endProperty, new Vector3(20f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void RaycastTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var originProperty = new BlackboardPropertyName("origin");
			var directionProperty = new BlackboardPropertyName("direction");
			const float maxDistance = 15f;
			LayerMask layerMask = 1;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<Raycast, BlackboardPropertyName, BlackboardPropertyName, float, LayerMask>(
					originProperty, directionProperty, maxDistance, layerMask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(15f, 0f, 0f));
			blackboard.SetStructValue(directionProperty, new Vector3(-1f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(directionProperty, new Vector3(1f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void RaycastVariableTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var originProperty = new BlackboardPropertyName("origin");
			var directionProperty = new BlackboardPropertyName("direction");
			var maxDistanceProperty = new BlackboardPropertyName("maxDistance");
			var layerMaskProperty = new BlackboardPropertyName("layerMask");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<RaycastVariable, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName, BlackboardPropertyName>(originProperty, directionProperty,
				maxDistanceProperty, layerMaskProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(15f, 0f, 0f));
			blackboard.SetStructValue(directionProperty, new Vector3(-1f, 0f, 0f));
			blackboard.SetStructValue(maxDistanceProperty, 15f);
			blackboard.SetStructValue(layerMaskProperty, (LayerMask)1);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(directionProperty, new Vector3(1f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void SphereCastTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var originProperty = new BlackboardPropertyName("origin");
			var radiusProperty = new BlackboardPropertyName("radius");
			var directionProperty = new BlackboardPropertyName("direction");
			const float maxDistance = 15f;
			LayerMask layerMask = 1;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SphereCast, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName, float, LayerMask>(originProperty, radiusProperty, directionProperty,
				maxDistance, layerMask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(20f, 0f, 0f));
			blackboard.SetStructValue(radiusProperty, 5f);
			blackboard.SetStructValue(directionProperty, new Vector3(-1f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(directionProperty, new Vector3(1f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void SphereCastVariableTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var originProperty = new BlackboardPropertyName("origin");
			var radiusProperty = new BlackboardPropertyName("radius");
			var directionProperty = new BlackboardPropertyName("direction");
			var maxDistanceProperty = new BlackboardPropertyName("maxDistance");
			var layerMaskProperty = new BlackboardPropertyName("layerMask");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SphereCastVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(originProperty, radiusProperty,
					directionProperty, maxDistanceProperty, layerMaskProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(20f, 0f, 0f));
			blackboard.SetStructValue(radiusProperty, 5f);
			blackboard.SetStructValue(directionProperty, new Vector3(-1f, 0f, 0f));
			blackboard.SetStructValue(maxDistanceProperty, 15f);
			blackboard.SetStructValue(layerMaskProperty, (LayerMask)1);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(directionProperty, new Vector3(1f, 0f, 0f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CheckBoxTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var centerProperty = new BlackboardPropertyName("center");
			var halfExtentsProperty = new BlackboardPropertyName("half extents");
			var orientationProperty = new BlackboardPropertyName("orientation");
			LayerMask layerMask = 1;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CheckBox, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
				LayerMask>(centerProperty, halfExtentsProperty, orientationProperty, layerMask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(centerProperty, new Vector3(25f, 0f, 0f));
			blackboard.SetStructValue(halfExtentsProperty, new Vector3(5f, 5f, 5f));
			blackboard.SetStructValue(orientationProperty, Quaternion.identity);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(centerProperty, new Vector3(11f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CheckBoxVariableTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var centerProperty = new BlackboardPropertyName("center");
			var halfExtentsProperty = new BlackboardPropertyName("half extents");
			var orientationProperty = new BlackboardPropertyName("orientation");
			var layerMaskProperty = new BlackboardPropertyName("layerMask");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CheckBoxVariable, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName, BlackboardPropertyName>(centerProperty, halfExtentsProperty,
					orientationProperty, layerMaskProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(centerProperty, new Vector3(25f, 0f, 0f));
			blackboard.SetStructValue(halfExtentsProperty, new Vector3(5f, 5f, 5f));
			blackboard.SetStructValue(orientationProperty, Quaternion.identity);
			blackboard.SetStructValue(layerMaskProperty, (LayerMask)1);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(centerProperty, new Vector3(11f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CheckCapsuleTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var point1Property = new BlackboardPropertyName("point1");
			var point2Property = new BlackboardPropertyName("point2");
			var radiusProperty = new BlackboardPropertyName("radius");
			LayerMask layerMask = 1;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CheckCapsule, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
				LayerMask>(point1Property, point2Property, radiusProperty, layerMask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(point1Property, new Vector3(20f, 5f, 0f));
			blackboard.SetStructValue(point2Property, new Vector3(20f, -5f, 0f));
			blackboard.SetStructValue(radiusProperty, 5f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(radiusProperty, 15f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CheckCapsuleVariableTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var point1Property = new BlackboardPropertyName("point1");
			var point2Property = new BlackboardPropertyName("point2");
			var radiusProperty = new BlackboardPropertyName("radius");
			var layerMaskProperty = new BlackboardPropertyName("layerMask");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CheckCapsuleVariable, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName, BlackboardPropertyName>(point1Property, point2Property, radiusProperty,
				layerMaskProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(point1Property, new Vector3(20f, 5f, 0f));
			blackboard.SetStructValue(point2Property, new Vector3(20f, -5f, 0f));
			blackboard.SetStructValue(radiusProperty, 5f);
			blackboard.SetStructValue(layerMaskProperty, (LayerMask)1);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(radiusProperty, 15f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CheckSphereTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var originProperty = new BlackboardPropertyName("origin");
			var radiusProperty = new BlackboardPropertyName("radius");
			LayerMask layerMask = 1;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CheckSphere, BlackboardPropertyName, BlackboardPropertyName, LayerMask>(originProperty,
				radiusProperty, layerMask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(20f, 0f, 0f));
			blackboard.SetStructValue(radiusProperty, 5f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(11f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void CheckSphereVariableTest()
		{
			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.radius = 10f;

			var originProperty = new BlackboardPropertyName("origin");
			var radiusProperty = new BlackboardPropertyName("radius");
			var layerMaskProperty = new BlackboardPropertyName("layerMask");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<CheckSphereVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(originProperty, radiusProperty, layerMaskProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(20f, 0f, 0f));
			blackboard.SetStructValue(radiusProperty, 5f);
			blackboard.SetStructValue(layerMaskProperty, (LayerMask)1);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(originProperty, new Vector3(11f, 0f, 0f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

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
		public static void IsColorAlphaGreaterTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float alpha = 0.6f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColorAlphaGreater, BlackboardPropertyName, float>(colorProperty, alpha).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.8f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColorAlphaGreaterVariable()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var alphaProperty = new BlackboardPropertyName("alpha");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColorAlphaGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(colorProperty,
					alphaProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(alphaProperty, 0.6f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.8f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsColorAlphaLessTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float alpha = 0.6f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColorAlphaLess, BlackboardPropertyName, float>(colorProperty, alpha).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.8f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColorAlphaLessVariable()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var alphaProperty = new BlackboardPropertyName("alpha");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColorAlphaLessVariable, BlackboardPropertyName, BlackboardPropertyName>(colorProperty,
					alphaProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(alphaProperty, 0.6f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.8f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsColorBlueGreaterTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float blue = 0.6f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColorBlueGreater, BlackboardPropertyName, float>(colorProperty, blue).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.8f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColorBlueGreaterVariable()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blueProperty = new BlackboardPropertyName("blue");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColorBlueGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(colorProperty,
					blueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(blueProperty, 0.6f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.8f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsColorBlueLessTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float blue = 0.6f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColorBlueLess, BlackboardPropertyName, float>(colorProperty, blue).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.8f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColorBlueLessVariable()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blueProperty = new BlackboardPropertyName("blue");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColorBlueLessVariable, BlackboardPropertyName, BlackboardPropertyName>(colorProperty,
					blueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(blueProperty, 0.6f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.8f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsColorGreenGreaterTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float green = 0.6f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColorGreenGreater, BlackboardPropertyName, float>(colorProperty, green).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.8f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColorGreenGreaterVariable()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var greenProperty = new BlackboardPropertyName("green");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColorGreenGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(colorProperty,
					greenProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(greenProperty, 0.6f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.8f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsColorGreenLessTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float green = 0.6f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColorGreenLess, BlackboardPropertyName, float>(colorProperty, green).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.8f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColorGreenLessVariable()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var greenProperty = new BlackboardPropertyName("green");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColorGreenLessVariable, BlackboardPropertyName, BlackboardPropertyName>(colorProperty,
					greenProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(greenProperty, 0.6f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.8f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsColorRedGreaterTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float red = 0.6f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColorRedGreater, BlackboardPropertyName, float>(colorProperty, red).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.8f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColorRedGreaterVariable()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var redProperty = new BlackboardPropertyName("red");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColorRedGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(colorProperty,
					redProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(redProperty, 0.6f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.8f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsColorRedLessTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float red = 0.6f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsColorRedLess, BlackboardPropertyName, float>(colorProperty, red).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.8f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsColorRedLessVariable()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var redProperty = new BlackboardPropertyName("red");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsColorRedLessVariable, BlackboardPropertyName, BlackboardPropertyName>(colorProperty,
					redProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(redProperty, 0.6f);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.5f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, new Color(0.8f, 0.5f, 0.5f, 0.5f));
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsCursorLockStateEqualTest()
		{
			const CursorLockMode lockState = CursorLockMode.Confined;
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsCursorLockStateEqual, CursorLockMode>(lockState).Complete();
			TreeRoot treeRoot = treeBuilder.Build();
			treeRoot.Initialize();

			Cursor.lockState = lockState;
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			Cursor.lockState = CursorLockMode.None;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsCursorVisibleTest()
		{
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsCursorVisible>().Complete();
			TreeRoot treeRoot = treeBuilder.Build();
			treeRoot.Initialize();

			Cursor.visible = false;
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			Cursor.visible = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsLayerMaskInMask()
		{
			var valueProperty = new BlackboardPropertyName("value");
			LayerMask mask = 14;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsLayerMaskInMask, BlackboardPropertyName, LayerMask>(valueProperty, mask).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			LayerMask value = 6;
			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = 1;
			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsLayerMaskInMaskVariable()
		{
			var valueProperty = new BlackboardPropertyName("value");
			var maskProperty = new BlackboardPropertyName("mask");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();

			treeBuilder
				.AddLeaf<IsLayerMaskInMaskVariable, BlackboardPropertyName, BlackboardPropertyName>(valueProperty,
					maskProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			LayerMask value = 6;
			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			LayerMask mask = 14;
			blackboard.SetStructValue(maskProperty, mask);
			blackboard.RemoveStruct<LayerMask>(valueProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			value = 1;
			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsLayerMaskEqualTest()
		{
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsLayerMaskValueEqual, LayerMask, BlackboardPropertyName>(14, valueProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			LayerMask value = 6;
			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = 14;
			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsLayerMaskEqualVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("firstProperty");
			var secondProperty = new BlackboardPropertyName("secondProperty");
			LayerMask firstValue = 14;
			LayerMask secondValue = 1;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsLayerMaskValueEqualVariable, BlackboardPropertyName, BlackboardPropertyName>(firstProperty,
					secondProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveStruct<LayerMask>(firstProperty);
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
		public static void IsObjectAliveTest()
		{
			var objectProperty = new BlackboardPropertyName("object");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsObjectAlive, BlackboardPropertyName>(objectProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<GameObject>(objectProperty, null);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			var gameObject = new GameObject();
			blackboard.SetClassValue(objectProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			Object.DestroyImmediate(gameObject);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

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
		
		[Test]
		public static void IsLayerMaskIntersectsTest()
		{
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<LayerMaskIntersects, BlackboardPropertyName, LayerMask>(valueProperty, 14).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			LayerMask value = 1;
			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			value = 2;
			blackboard.SetStructValue(valueProperty, value);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsLayerMaskIntersectsVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("firstProperty");
			var secondProperty = new BlackboardPropertyName("secondProperty");
			LayerMask firstValue = 14;
			LayerMask secondValue = 1;
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<LayerMaskIntersectsVariable, BlackboardPropertyName, BlackboardPropertyName>(firstProperty,
					secondProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			blackboard.RemoveStruct<LayerMask>(firstProperty);
			blackboard.SetStructValue(secondProperty, secondValue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, firstValue);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			secondValue = 2;
			blackboard.SetStructValue(secondProperty, secondValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, secondValue);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
	}
}
