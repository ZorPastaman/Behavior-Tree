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
		public static void IsQuaternionAngleGreaterTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			const float angle = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionAngleGreater, BlackboardPropertyName, BlackboardPropertyName, float>(
				firstProperty, secondProperty, angle).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion first = Quaternion.Euler(150f, 30f, 10f);
			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion second = Quaternion.Euler(150f, 30f, 110f);
			blackboard.SetStructValue(secondProperty, second);
			blackboard.RemoveStruct<Quaternion>(firstProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			second = Quaternion.Euler(150f, 30f, 30f);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionAngleGreaterVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			var angleProperty = new BlackboardPropertyName("angle");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionAngleGreaterVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(firstProperty, secondProperty, angleProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion first = Quaternion.Euler(150f, 30f, 10f);
			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion second = Quaternion.Euler(150f, 30f, 110f);
			blackboard.SetStructValue(secondProperty, second);
			blackboard.RemoveStruct<Quaternion>(firstProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float angle = 50f;
			blackboard.SetStructValue(angleProperty, angle);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			second = Quaternion.Euler(150f, 30f, 30f);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsQuaternionAngleLessTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			const float angle = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionAngleLess, BlackboardPropertyName, BlackboardPropertyName, float>(
				firstProperty, secondProperty, angle).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion first = Quaternion.Euler(150f, 30f, 10f);
			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion second = Quaternion.Euler(150f, 30f, 110f);
			blackboard.SetStructValue(secondProperty, second);
			blackboard.RemoveStruct<Quaternion>(firstProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			second = Quaternion.Euler(150f, 30f, 30f);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionAngleLessVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			var angleProperty = new BlackboardPropertyName("angle");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionAngleLessVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(firstProperty, secondProperty, angleProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion first = Quaternion.Euler(150f, 30f, 10f);
			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion second = Quaternion.Euler(150f, 30f, 110f);
			blackboard.SetStructValue(secondProperty, second);
			blackboard.RemoveStruct<Quaternion>(firstProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float angle = 50f;
			blackboard.SetStructValue(angleProperty, angle);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			second = Quaternion.Euler(150f, 30f, 30f);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsQuaternionDotGreaterTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			const float dot = 0.5f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionDotGreater, BlackboardPropertyName, BlackboardPropertyName, float>(
				firstProperty, secondProperty, dot).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion first = Quaternion.Euler(150f, 30f, 10f);
			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion second = Quaternion.Euler(150f, 30f, 75f);
			blackboard.SetStructValue(secondProperty, second);
			blackboard.RemoveStruct<Quaternion>(firstProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			second = Quaternion.Euler(150f, 30f, 180f);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionDotGreaterVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			var dotProperty = new BlackboardPropertyName("dot");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionDotGreaterVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(firstProperty, secondProperty, dotProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion first = Quaternion.Euler(150f, 30f, 10f);
			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion second = Quaternion.Euler(150f, 30f, 75f);
			blackboard.SetStructValue(secondProperty, second);
			blackboard.RemoveStruct<Quaternion>(firstProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float dot = 0.5f;
			blackboard.SetStructValue(dotProperty, dot);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			second = Quaternion.Euler(150f, 30f, 180f);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsQuaternionDotLessTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			const float dot = 0.5f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionDotLess, BlackboardPropertyName, BlackboardPropertyName, float>(
				firstProperty, secondProperty, dot).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion first = Quaternion.Euler(150f, 30f, 10f);
			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion second = Quaternion.Euler(150f, 30f, 75f);
			blackboard.SetStructValue(secondProperty, second);
			blackboard.RemoveStruct<Quaternion>(firstProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			second = Quaternion.Euler(150f, 30f, 180f);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionDotLessVariableTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			var dotProperty = new BlackboardPropertyName("dot");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionDotLessVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(firstProperty, secondProperty, dotProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion first = Quaternion.Euler(150f, 30f, 10f);
			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			Quaternion second = Quaternion.Euler(150f, 30f, 75f);
			blackboard.SetStructValue(secondProperty, second);
			blackboard.RemoveStruct<Quaternion>(firstProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float dot = 0.5f;
			blackboard.SetStructValue(dotProperty, dot);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			second = Quaternion.Euler(150f, 30f, 180f);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionWGreaterTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			const float value = 2f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionWGreater, BlackboardPropertyName, float>(quaternionProperty, value)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			quaternion.w = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionWGreaterVariableTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionWGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
					quaternionProperty, valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float value = 2f;
			blackboard.SetStructValue(valueProperty, value);
			blackboard.RemoveStruct<Quaternion>(quaternionProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			quaternion.w = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsQuaternionWLessTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			const float value = 2f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionWLess, BlackboardPropertyName, float>(quaternionProperty, value)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			quaternion.w = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionWLessVariableTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionWLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
					quaternionProperty, valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float value = 2f;
			blackboard.SetStructValue(valueProperty, value);
			blackboard.RemoveStruct<Quaternion>(quaternionProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			quaternion.w = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionXGreaterTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			const float value = 20f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionXGreater, BlackboardPropertyName, float>(quaternionProperty, value)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			quaternion.x = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionXGreaterVariableTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionXGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
					quaternionProperty, valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float value = 20f;
			blackboard.SetStructValue(valueProperty, value);
			blackboard.RemoveStruct<Quaternion>(quaternionProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			quaternion.x = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionXLessTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			const float value = 20f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionXLess, BlackboardPropertyName, float>(quaternionProperty, value)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			quaternion.x = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionXLessVariableTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionXLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
					quaternionProperty, valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float value = 20f;
			blackboard.SetStructValue(valueProperty, value);
			blackboard.RemoveStruct<Quaternion>(quaternionProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			quaternion.x = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionYGreaterTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			const float value = 20f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionYGreater, BlackboardPropertyName, float>(quaternionProperty, value)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			quaternion.y = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionYGreaterVariableTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionYGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
					quaternionProperty, valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float value = 20f;
			blackboard.SetStructValue(valueProperty, value);
			blackboard.RemoveStruct<Quaternion>(quaternionProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			quaternion.y = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionYLessTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			const float value = 20f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionYLess, BlackboardPropertyName, float>(quaternionProperty, value)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			quaternion.y = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionYLessVariableTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionYLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
					quaternionProperty, valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float value = 20f;
			blackboard.SetStructValue(valueProperty, value);
			blackboard.RemoveStruct<Quaternion>(quaternionProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			quaternion.y = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionZGreaterTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			const float value = 60f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionZGreater, BlackboardPropertyName, float>(quaternionProperty, value)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			quaternion.z = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionZGreaterVariableTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionZGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
					quaternionProperty, valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float value = 60f;
			blackboard.SetStructValue(valueProperty, value);
			blackboard.RemoveStruct<Quaternion>(quaternionProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			quaternion.z = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionZLessTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			const float value = 60f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionZLess, BlackboardPropertyName, float>(quaternionProperty, value)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			quaternion.z = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsQuaternionZLessVariableTest()
		{
			var quaternionProperty = new BlackboardPropertyName("quaternion");
			var valueProperty = new BlackboardPropertyName("value");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsQuaternionZLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
					quaternionProperty, valueProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var quaternion = new Quaternion(50f, 30f, 70f, 5f);
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float value = 60f;
			blackboard.SetStructValue(valueProperty, value);
			blackboard.RemoveStruct<Quaternion>(quaternionProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			quaternion.z = 0f;
			blackboard.SetStructValue(quaternionProperty, quaternion);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsRaycastHitDistanceGreaterTest()
		{
			var raycastProperty = new BlackboardPropertyName("raycast");
			const float distance = 30f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsRaycastHitDistanceGreater, BlackboardPropertyName, float>(raycastProperty, distance)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var raycast = new RaycastHit { distance = 50f };
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			raycast.distance = 10f;
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsRaycastHitDistanceGreaterVariableTest()
		{
			var raycastProperty = new BlackboardPropertyName("raycast");
			var distanceProperty = new BlackboardPropertyName("distance");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsRaycastHitDistanceGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
					raycastProperty, distanceProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var raycast = new RaycastHit { distance = 50f };
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float distance = 30f;
			blackboard.SetStructValue(distanceProperty, distance);
			blackboard.RemoveStruct<RaycastHit>(raycastProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			raycast.distance = 10f;
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsRaycastHitDistanceLessTest()
		{
			var raycastProperty = new BlackboardPropertyName("raycast");
			const float distance = 30f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsRaycastHitDistanceLess, BlackboardPropertyName, float>(raycastProperty, distance)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var raycast = new RaycastHit { distance = 50f };
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			raycast.distance = 10f;
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsRaycastHitDistanceLessVariableTest()
		{
			var raycastProperty = new BlackboardPropertyName("raycast");
			var distanceProperty = new BlackboardPropertyName("distance");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsRaycastHitDistanceLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
					raycastProperty, distanceProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var raycast = new RaycastHit { distance = 50f };
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float distance = 30f;
			blackboard.SetStructValue(distanceProperty, distance);
			blackboard.RemoveStruct<RaycastHit>(raycastProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			raycast.distance = 10f;
			blackboard.SetStructValue(raycastProperty, raycast);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}

		[Test]
		public static void IsRigidbodyVelocityGreaterTest()
		{
			var rigidbodyProperty = new BlackboardPropertyName("rigidbody");
			const float velocity = 100f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsRigidbodyVelocityGreater, BlackboardPropertyName, float>(rigidbodyProperty, velocity)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var rigidbody = new GameObject().AddComponent<Rigidbody>();
			rigidbody.velocity = new Vector3(150f, 0f, 0f);
			blackboard.SetClassValue(rigidbodyProperty, rigidbody);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			rigidbody.velocity = new Vector3(30f, 0f, 0f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsRigidbodyVelocityGreaterVariableTest()
		{
			var rigidbodyProperty = new BlackboardPropertyName("rigidbody");
			var velocityProperty = new BlackboardPropertyName("velocity");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsRigidbodyVelocityGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
				rigidbodyProperty, velocityProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var rigidbody = new GameObject().AddComponent<Rigidbody>();
			rigidbody.velocity = new Vector3(150f, 0f, 0f);
			blackboard.SetClassValue(rigidbodyProperty, rigidbody);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float velocity = 100f;
			blackboard.SetStructValue(velocityProperty, velocity);
			blackboard.RemoveObject<Rigidbody>(rigidbodyProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(rigidbodyProperty, rigidbody);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			rigidbody.velocity = new Vector3(30f, 0f, 0f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsRigidbodyVelocityLessTest()
		{
			var rigidbodyProperty = new BlackboardPropertyName("rigidbody");
			const float velocity = 100f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsRigidbodyVelocityLess, BlackboardPropertyName, float>(rigidbodyProperty, velocity)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var rigidbody = new GameObject().AddComponent<Rigidbody>();
			rigidbody.velocity = new Vector3(150f, 0f, 0f);
			blackboard.SetClassValue(rigidbodyProperty, rigidbody);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			rigidbody.velocity = new Vector3(30f, 0f, 0f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsRigidbodyVelocityLessVariableTest()
		{
			var rigidbodyProperty = new BlackboardPropertyName("rigidbody");
			var velocityProperty = new BlackboardPropertyName("velocity");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsRigidbodyVelocityLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
				rigidbodyProperty, velocityProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var rigidbody = new GameObject().AddComponent<Rigidbody>();
			rigidbody.velocity = new Vector3(150f, 0f, 0f);
			blackboard.SetClassValue(rigidbodyProperty, rigidbody);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			const float velocity = 100f;
			blackboard.SetStructValue(velocityProperty, velocity);
			blackboard.RemoveObject<Rigidbody>(rigidbodyProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(rigidbodyProperty, rigidbody);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			rigidbody.velocity = new Vector3(30f, 0f, 0f);
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

		[Test]
		public static void IsVector2AngleGreaterTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			const float angle = 90f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsVector2AngleGreater, BlackboardPropertyName, BlackboardPropertyName, float>(fromProperty,
					toProperty, angle).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector2(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector2(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector2>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector2(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector2AngleGreaterVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var angleProperty = new BlackboardPropertyName("angle");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2AngleGreaterVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(fromProperty, toProperty, angleProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector2(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector2(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector2>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(angleProperty, 90f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector2(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(angleProperty, 20f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector2(1, 0.1f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2AngleLessTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			const float angle = 90f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsVector2AngleLess, BlackboardPropertyName, BlackboardPropertyName, float>(fromProperty,
					toProperty, angle).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector2(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector2(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector2>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector2(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector2AngleLessVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var angleProperty = new BlackboardPropertyName("angle");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2AngleLessVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(fromProperty, toProperty, angleProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector2(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector2(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector2>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(angleProperty, 90f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector2(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(angleProperty, 20f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector2(1, 0.1f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2DotGreaterTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			const float dot = 0f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsVector2DotGreater, BlackboardPropertyName, BlackboardPropertyName, float>(fromProperty,
					toProperty, dot).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector2(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector2(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector2>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector2(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector2DotGreaterVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var dotProperty = new BlackboardPropertyName("dot");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2DotGreaterVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(fromProperty, toProperty, dotProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector2(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector2(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector2>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(dotProperty, 0f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector2(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(dotProperty, 0.6f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector2(1, 0.1f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2DotLessTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			const float dot = 0f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsVector2DotLess, BlackboardPropertyName, BlackboardPropertyName, float>(fromProperty,
					toProperty, dot).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector2(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector2(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector2>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector2(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector2DotLessVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var dotProperty = new BlackboardPropertyName("dot");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2DotLessVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(fromProperty, toProperty, dotProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector2(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector2(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector2>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(dotProperty, 0f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector2(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(dotProperty, 0.6f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector2(1, 0.1f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector2MagnitudeGreaterTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float magnitude = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2MagnitudeGreater, BlackboardPropertyName, float>(vectorProperty, magnitude)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(40f, 0f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.y = 40f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2MagnitudeGreaterVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var magnitudeProperty = new BlackboardPropertyName("magnitude");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2MagnitudeGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, magnitudeProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(40f, 0f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(magnitudeProperty, 40f);
			blackboard.RemoveStruct<Vector2>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.y = 40f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2MagnitudeLessTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float magnitude = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2MagnitudeLess, BlackboardPropertyName, float>(vectorProperty, magnitude)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(40f, 0f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.y = 40f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2MagnitudeLessVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var magnitudeProperty = new BlackboardPropertyName("magnitude");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2MagnitudeLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, magnitudeProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(40f, 0f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(magnitudeProperty, 50f);
			blackboard.RemoveStruct<Vector2>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.y = 40f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector2XGreaterTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float x = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2XGreater, BlackboardPropertyName, float>(vectorProperty, x).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.x = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2XGreaterVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var xProperty = new BlackboardPropertyName("x");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2XGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, xProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(xProperty, 50f);
			blackboard.RemoveStruct<Vector2>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.x = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2XLessTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float x = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2XLess, BlackboardPropertyName, float>(vectorProperty, x).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.x = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2XLessVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var xProperty = new BlackboardPropertyName("x");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2XLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, xProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(xProperty, 50f);
			blackboard.RemoveStruct<Vector2>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.x = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2YGreaterTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float y = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2YGreater, BlackboardPropertyName, float>(vectorProperty, y).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.y = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2YGreaterVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var yProperty = new BlackboardPropertyName("y");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2YGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, yProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(yProperty, 50f);
			blackboard.RemoveStruct<Vector2>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.y = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2YLessTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float y = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2YLess, BlackboardPropertyName, float>(vectorProperty, y).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.y = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector2YLessVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var yProperty = new BlackboardPropertyName("y");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector2YLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, yProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector2(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(yProperty, 50f);
			blackboard.RemoveStruct<Vector2>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.y = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3AngleGreaterTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			const float angle = 90f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsVector3AngleGreater, BlackboardPropertyName, BlackboardPropertyName, float>(fromProperty,
					toProperty, angle).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector3(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector3(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector3>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector3(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3AngleGreaterVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var angleProperty = new BlackboardPropertyName("angle");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3AngleGreaterVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(fromProperty, toProperty, angleProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector3(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector3(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector3>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(angleProperty, 90f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector3(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(angleProperty, 20f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector3(1, 0.1f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3AngleLessTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			const float angle = 90f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsVector3AngleLess, BlackboardPropertyName, BlackboardPropertyName, float>(fromProperty,
					toProperty, angle).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector3(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector3(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector3>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector3(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3AngleLessVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var angleProperty = new BlackboardPropertyName("angle");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3AngleLessVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(fromProperty, toProperty, angleProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector3(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector3(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector3>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(angleProperty, 90f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector3(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(angleProperty, 20f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector3(1, 0.1f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3DotGreaterTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			const float dot = 0f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsVector3DotGreater, BlackboardPropertyName, BlackboardPropertyName, float>(fromProperty,
					toProperty, dot).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector3(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector3(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector3>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector3(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3DotGreaterVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var dotProperty = new BlackboardPropertyName("dot");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3DotGreaterVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(fromProperty, toProperty, dotProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector3(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector3(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector3>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(dotProperty, 0f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector3(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			blackboard.SetStructValue(dotProperty, 0.6f);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			to = new Vector3(1, 0.1f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3DotLessTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			const float dot = 0f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<IsVector3DotLess, BlackboardPropertyName, BlackboardPropertyName, float>(fromProperty,
					toProperty, dot).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector3(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector3(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector3>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector3(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3DotLessVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var dotProperty = new BlackboardPropertyName("dot");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3DotLessVariable, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName>(fromProperty, toProperty, dotProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var from = new Vector3(1f, 0f);
			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var to = new Vector3(-1f, 0f);
			blackboard.SetStructValue(toProperty, to);
			blackboard.RemoveStruct<Vector3>(fromProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(dotProperty, 0f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector3(0.5f, 0.5f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			blackboard.SetStructValue(dotProperty, 0.6f);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			to = new Vector3(1, 0.1f);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3MagnitudeGreaterTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float magnitude = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3MagnitudeGreater, BlackboardPropertyName, float>(vectorProperty, magnitude)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(40f, 0f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.y = 40f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3MagnitudeGreaterVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var magnitudeProperty = new BlackboardPropertyName("magnitude");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3MagnitudeGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, magnitudeProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(40f, 0f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(magnitudeProperty, 40f);
			blackboard.RemoveStruct<Vector3>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.y = 40f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3MagnitudeLessTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float magnitude = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3MagnitudeLess, BlackboardPropertyName, float>(vectorProperty, magnitude)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(40f, 0f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.y = 40f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3MagnitudeLessVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var magnitudeProperty = new BlackboardPropertyName("magnitude");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3MagnitudeLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, magnitudeProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(40f, 0f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(magnitudeProperty, 50f);
			blackboard.RemoveStruct<Vector3>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.y = 40f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3XGreaterTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float x = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3XGreater, BlackboardPropertyName, float>(vectorProperty, x).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.x = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3XGreaterVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var xProperty = new BlackboardPropertyName("x");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3XGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, xProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(xProperty, 50f);
			blackboard.RemoveStruct<Vector3>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.x = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3XLessTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float x = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3XLess, BlackboardPropertyName, float>(vectorProperty, x).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.x = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3XLessVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var xProperty = new BlackboardPropertyName("x");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3XLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, xProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(xProperty, 50f);
			blackboard.RemoveStruct<Vector3>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.x = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3YGreaterTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float y = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3YGreater, BlackboardPropertyName, float>(vectorProperty, y).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.y = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3YGreaterVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var yProperty = new BlackboardPropertyName("y");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3YGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, yProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(yProperty, 50f);
			blackboard.RemoveStruct<Vector3>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.y = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3YLessTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float y = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3YLess, BlackboardPropertyName, float>(vectorProperty, y).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.y = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void IsVector3YLessVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var yProperty = new BlackboardPropertyName("y");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3YLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, yProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(yProperty, 50f);
			blackboard.RemoveStruct<Vector3>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.y = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			
			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3ZGreaterTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float z = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3ZGreater, BlackboardPropertyName, float>(vectorProperty, z).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f, 100f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.z = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3ZGreaterVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var zProperty = new BlackboardPropertyName("z");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3ZGreaterVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, zProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f, 100f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(zProperty, 50f);
			blackboard.RemoveStruct<Vector3>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			vector.z = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3ZLessTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			const float z = 50f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3ZLess, BlackboardPropertyName, float>(vectorProperty, z).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f, 100f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.z = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

			treeRoot.Dispose();
		}

		[Test]
		public static void IsVector3ZLessVariableTest()
		{
			var vectorProperty = new BlackboardPropertyName("vector");
			var zProperty = new BlackboardPropertyName("z");
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<IsVector3ZLessVariable, BlackboardPropertyName, BlackboardPropertyName>(
				vectorProperty, zProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var vector = new Vector3(100f, 200f, 100f);
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(zProperty, 50f);
			blackboard.RemoveStruct<Vector3>(vectorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Failure, treeRoot.Tick());

			vector.z = 0f;
			blackboard.SetStructValue(vectorProperty, vector);
			Assert.AreEqual(Status.Success, treeRoot.Tick());

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
