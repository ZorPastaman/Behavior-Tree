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
		public static void AddColorsTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var first = new Color(0.3f, 0.35f, 0.25f, 0.4f);
			var second = new Color(0.4f, 0.2f, 0.5f, 0.45f);
			Color result = first + second;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<AddColors, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					firstProperty, secondProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(firstProperty);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color res));
			Assert.AreEqual(result, res);

			treeRoot.Dispose();
		}

		[Test]
		public static void AddComponentTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var componentProperty = new BlackboardPropertyName("component");
			var blackboard = new Blackboard();
			var gameObject = new GameObject();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<AddComponent<Rigidbody>, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					componentProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Component>(componentProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Component>(componentProperty));

			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(componentProperty, out Rigidbody rigidbody));
			Assert.AreNotEqual(null, rigidbody);
			Assert.AreEqual(gameObject.GetComponent<Rigidbody>(), rigidbody);

			treeRoot.Dispose();
		}

		[Test]
		public static void ColorToVector4Test()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var vectorProperty = new BlackboardPropertyName("vector");
			var blackboard = new Blackboard();
			var color = new Color(0.3f, 0.7f, 0.9f, 0.7f);
			Vector4 vector = color;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<ColorToVector4, BlackboardPropertyName, BlackboardPropertyName>(colorProperty, vectorProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Vector4>(vectorProperty));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(vectorProperty, out Vector4 vec));
			Assert.AreEqual(vector, vec);

			treeRoot.Dispose();
		}

		[Test]
		public static void DivideColorAndNumberTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var color = new Color(0.8f, 0.3f, 0.95f, 0.9f);
			const float number = 3f;
			Color result = color / number;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<DivideColorAndNumber, BlackboardPropertyName, float, BlackboardPropertyName>(colorProperty,
					number, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color res));
			Assert.AreEqual(result, res);

			treeRoot.Dispose();
		}

		[Test]
		public static void DivideColorAndNumberVariableTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var numberProperty = new BlackboardPropertyName("number");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var color = new Color(0.8f, 0.3f, 0.95f, 0.9f);
			const float number = 3f;
			Color result = color / number;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<DivideColorAndNumberVariable, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName>(colorProperty, numberProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(numberProperty, number);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color res));
			Assert.AreEqual(result, res);

			treeRoot.Dispose();
		}

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
		public static void GetColliderIsTriggerTest()
		{
			var colliderProperty = new BlackboardPropertyName("collider");
			var isTriggerProperty = new BlackboardPropertyName("isTrigger");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<GetColliderIsTrigger, BlackboardPropertyName, BlackboardPropertyName>(colliderProperty,
				isTriggerProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(isTriggerProperty));

			blackboard.SetClassValue<Collider>(colliderProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(isTriggerProperty));

			var collider = new GameObject().AddComponent<SphereCollider>();
			collider.isTrigger = false;
			blackboard.SetClassValue(colliderProperty, collider);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(isTriggerProperty, out bool isTrigger));
			Assert.AreEqual(false, isTrigger);

			collider.isTrigger = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(isTriggerProperty, out isTrigger));
			Assert.AreEqual(true, isTrigger);

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
		public static void GetColorAlphaTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var alphaProperty = new BlackboardPropertyName("alpha");
			var blackboard = new Blackboard();
			var color = new Color(0.55f, 0.45f, 0.35f, 0.65f);

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetColorAlpha, BlackboardPropertyName, BlackboardPropertyName>(colorProperty, alphaProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(alphaProperty, out float alpha));
			Assert.AreEqual(color.a, alpha);
			
			treeRoot.Dispose();
		}

		[Test]
		public static void GetColorBlueTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blueProperty = new BlackboardPropertyName("blue");
			var blackboard = new Blackboard();
			var color = new Color(0.55f, 0.45f, 0.35f, 0.65f);

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetColorBlue, BlackboardPropertyName, BlackboardPropertyName>(colorProperty, blueProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(blueProperty, out float blue));
			Assert.AreEqual(color.b, blue);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetColorGreenTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var greenProperty = new BlackboardPropertyName("green");
			var blackboard = new Blackboard();
			var color = new Color(0.55f, 0.45f, 0.35f, 0.65f);

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetColorGreen, BlackboardPropertyName, BlackboardPropertyName>(colorProperty, greenProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(greenProperty, out float green));
			Assert.AreEqual(color.g, green);
			
			treeRoot.Dispose();
		}

		[Test]
		public static void GetColorRedTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var redProperty = new BlackboardPropertyName("red");
			var blackboard = new Blackboard();
			var color = new Color(0.55f, 0.45f, 0.35f, 0.65f);

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetColorRed, BlackboardPropertyName, BlackboardPropertyName>(colorProperty, redProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(redProperty, out float red));
			Assert.AreEqual(color.r, red);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetComponentTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var componentProperty = new BlackboardPropertyName("component");
			var blackboard = new Blackboard();
			var gameObject = new GameObject();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetComponent<Rigidbody>, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					componentProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Rigidbody>(componentProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Rigidbody>(componentProperty));

			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(componentProperty, out Rigidbody rigidbody));
			Assert.AreEqual(null, rigidbody);

			var rigid = gameObject.AddComponent<Rigidbody>();
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(componentProperty, out rigidbody));
			Assert.AreEqual(rigid, rigidbody);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetComponentGameObjectTest()
		{
			var componentProperty = new BlackboardPropertyName("component");
			var gameObjectProperty = new BlackboardPropertyName("gameObject");
			var blackboard = new Blackboard();
			var gameObject = new GameObject();
			Component component = gameObject.AddComponent<Rigidbody>();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetComponentGameObject, BlackboardPropertyName, BlackboardPropertyName>(componentProperty,
					gameObjectProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<GameObject>(gameObjectProperty));

			blackboard.SetClassValue<Component>(componentProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<GameObject>(gameObjectProperty));

			blackboard.SetClassValue(componentProperty, component);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(gameObjectProperty, out GameObject go));
			Assert.AreEqual(gameObject, go);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetComponentInChildrenTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var componentProperty = new BlackboardPropertyName("component");
			var blackboard = new Blackboard();
			var gameObject = new GameObject();
			var child = new GameObject();
			child.transform.SetParent(gameObject.transform);
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetComponentInChildren<Rigidbody>, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					componentProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Rigidbody>(componentProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Rigidbody>(componentProperty));

			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(componentProperty, out Rigidbody rigidbody));
			Assert.AreEqual(null, rigidbody);

			var rigid = child.AddComponent<Rigidbody>();
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(componentProperty, out rigidbody));
			Assert.AreEqual(rigid, rigidbody);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetComponentInParentTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var componentProperty = new BlackboardPropertyName("component");
			var blackboard = new Blackboard();
			var parent = new GameObject();
			var gameObject = new GameObject();
			gameObject.transform.SetParent(parent.transform);
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetComponentInParent<Rigidbody>, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					componentProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Rigidbody>(componentProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Rigidbody>(componentProperty));

			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(componentProperty, out Rigidbody rigidbody));
			Assert.AreEqual(null, rigidbody);

			var rigid = parent.AddComponent<Rigidbody>();
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(componentProperty, out rigidbody));
			Assert.AreEqual(rigid, rigidbody);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetComponentTransformTest()
		{
			var componentProperty = new BlackboardPropertyName("component");
			var transformProperty = new BlackboardPropertyName("transform");
			var blackboard = new Blackboard();
			var gameObject = new GameObject();
			Component component = gameObject.AddComponent<Rigidbody>();
			Transform transform = gameObject.transform;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetComponentTransform, BlackboardPropertyName, BlackboardPropertyName>(componentProperty,
					transformProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Transform>(transformProperty));

			blackboard.SetClassValue<Component>(componentProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Transform>(transformProperty));

			blackboard.SetClassValue(componentProperty, component);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(transformProperty, out Transform trans));
			Assert.AreEqual(transform, trans);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetCursorLockStateTest()
		{
			var lockStateProperty = new BlackboardPropertyName("lockState");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<GetCursorLockState, BlackboardPropertyName>(lockStateProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Cursor.lockState = CursorLockMode.None;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(lockStateProperty, out CursorLockMode lockState));
			Assert.AreEqual(CursorLockMode.None, lockState);

			Cursor.lockState = CursorLockMode.Locked;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(lockStateProperty, out lockState));
			Assert.AreEqual(CursorLockMode.Locked, lockState);

			Cursor.lockState = CursorLockMode.Confined;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(lockStateProperty, out lockState));
			Assert.AreEqual(CursorLockMode.Confined, lockState);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetCursorVisibleTest()
		{
			var visibleProperty = new BlackboardPropertyName("visible");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<GetCursorVisible, BlackboardPropertyName>(visibleProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Cursor.visible = false;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(visibleProperty, out bool visible));
			Assert.AreEqual(false, visible);

			Cursor.visible = true;
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(visibleProperty, out visible));
			Assert.AreEqual(true, visible);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetGameObjectIsActiveTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var isActiveProperty = new BlackboardPropertyName("isActive");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetGameObjectIsActive, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					isActiveProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(isActiveProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(isActiveProperty));

			var gameObject = new GameObject();
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(isActiveProperty, out bool isActive));
			Assert.AreEqual(true, isActive);

			gameObject.SetActive(false);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(isActiveProperty, out isActive));
			Assert.AreEqual(false, isActive);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetGameObjectIsActiveInHierarchyTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var isActiveProperty = new BlackboardPropertyName("isActive");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetGameObjectIsActiveInHierarchy, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					isActiveProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(isActiveProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<bool>(isActiveProperty));

			var parent = new GameObject();
			var gameObject = new GameObject();
			gameObject.transform.SetParent(parent.transform);
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(isActiveProperty, out bool isActive));
			Assert.AreEqual(true, isActive);

			parent.SetActive(false);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(isActiveProperty, out isActive));
			Assert.AreEqual(false, isActive);

			treeRoot.Dispose();
		}

		[Test]
		public static void GetGameObjectLayerTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var layerProperty = new BlackboardPropertyName("layer");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetGameObjectLayer, BlackboardPropertyName, BlackboardPropertyName>(goProperty, layerProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<int>(layerProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<int>(layerProperty));

			var gameObject = new GameObject {layer = 5};
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(layerProperty, out int layer));
			Assert.AreEqual(gameObject.layer, layer);

			treeRoot.Dispose();
		}
		
		[Test]
		public static void GetGameObjectTagTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var tagProperty = new BlackboardPropertyName("tag");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetGameObjectTag, BlackboardPropertyName, BlackboardPropertyName>(goProperty, tagProperty)
				.Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<string>(tagProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<string>(tagProperty));

			var gameObject = new GameObject {tag = "Player"};
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(tagProperty, out string tag));
			Assert.AreEqual(gameObject.tag, tag);

			treeRoot.Dispose();
		}
		
		[Test]
		public static void GetGameObjectTransformTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var transformProperty = new BlackboardPropertyName("transform");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<GetGameObjectTransform, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					transformProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Transform>(transformProperty));

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsObjectValue<Transform>(transformProperty));

			var gameObject = new GameObject();
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetClassValue(transformProperty, out Transform transform));
			Assert.AreEqual(gameObject.transform, transform);

			treeRoot.Dispose();
		}

		[Test]
		public static void LerpColorTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var resultProperty = new BlackboardPropertyName("result");
			var from = new Color(0.1f, 0.3f, 0.2f, 0.4f);
			var to = new Color(0.7f, 0.8f, 0.9f, 0.95f);
			const float time = 0.76f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<LerpColor, BlackboardPropertyName, BlackboardPropertyName, float, BlackboardPropertyName>(
					fromProperty, toProperty, time, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(fromProperty);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			Assert.AreEqual(Color.Lerp(from, to, time), result);

			treeRoot.Dispose();
		}

		[Test]
		public static void LerpColorVariableTest()
		{
			var fromProperty = new BlackboardPropertyName("from");
			var toProperty = new BlackboardPropertyName("to");
			var timeProperty = new BlackboardPropertyName("time");
			var resultProperty = new BlackboardPropertyName("result");
			var from = new Color(0.1f, 0.3f, 0.2f, 0.4f);
			var to = new Color(0.7f, 0.8f, 0.9f, 0.95f);
			const float time = 0.76f;
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<LerpColorVariable, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName>(fromProperty, toProperty, timeProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(fromProperty);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(toProperty);
			blackboard.SetStructValue(timeProperty, time);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<float>(timeProperty);
			blackboard.SetStructValue(toProperty, to);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(fromProperty);
			blackboard.SetStructValue(timeProperty, time);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(fromProperty, from);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			Assert.AreEqual(Color.Lerp(from, to, time), result);

			treeRoot.Dispose();
		}

		[Test]
		public static void MultiplyColorAndNumberTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var color = new Color(0.1f, 0.25f, 0.2f, 0.3f);
			const float number = 3f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<MultiplyColorAndNumber, BlackboardPropertyName, float, BlackboardPropertyName>(colorProperty,
					number, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			Assert.AreEqual(color * number, result);

			treeRoot.Dispose();
		}

		[Test]
		public static void MultiplyColorAndNumberVariableTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var numberProperty = new BlackboardPropertyName("number");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var color = new Color(0.1f, 0.25f, 0.2f, 0.3f);
			const float number = 3f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<MultiplyColorAndNumberVariable, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName>(colorProperty, numberProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(numberProperty, number);
			blackboard.RemoveStruct<Color>(colorProperty);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			Assert.AreEqual(color * number, result);

			treeRoot.Dispose();
		}

		[Test]
		public static void MultiplyColorsTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var first = new Color(0.5f, 0.45f, 0.44f, 0.4f);
			var second = new Color(0.55f, 0.35f, 0.37f, 0.3f);

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<MultiplyColors, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					firstProperty, secondProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(firstProperty);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			Assert.AreEqual(first * second, result);

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
		public static void SetColorTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();
			const float red = 0.6f;
			const float green = 0.7f;
			const float blue = 0.25f;
			const float alpha = 0.8f;

			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetColor, float, float, float, float, BlackboardPropertyName>(red, green, blue, alpha,
				colorProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(colorProperty, out Color color));
			Assert.AreEqual(new Color(red, green, blue, alpha), color);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetColorAlphaTest()
		{
			var colorProperty = new BlackboardPropertyName("property");
			var result = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var color = new Color(0.3f, 0.4f, 0.5f, 0.9f);
			const float alpha = 0.1f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorAlpha, BlackboardPropertyName, float, BlackboardPropertyName>(colorProperty, alpha,
					result).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(result));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(result, out Color col));
			color.a = alpha;
			Assert.AreEqual(color, col);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetColorAlphaVariableTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var alphaProperty = new BlackboardPropertyName("alpha");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			
			var color = new Color(0.3f, 0.4f, 0.5f, 0.9f);
			const float alpha = 0.1f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorAlphaVariable, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					colorProperty, alphaProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(alphaProperty, alpha);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			color.a = alpha;
			Assert.AreEqual(color, result);
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void SetColorBlueTest()
		{
			var colorProperty = new BlackboardPropertyName("property");
			var result = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var color = new Color(0.3f, 0.4f, 0.5f, 0.9f);
			const float blue = 0.1f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorBlue, BlackboardPropertyName, float, BlackboardPropertyName>(colorProperty, blue,
					result).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(result));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(result, out Color col));
			color.b = blue;
			Assert.AreEqual(color, col);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetColorBlueVariableTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var blueProperty = new BlackboardPropertyName("blue");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			
			var color = new Color(0.3f, 0.4f, 0.5f, 0.9f);
			const float blue = 0.1f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorBlueVariable, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					colorProperty, blueProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(blueProperty, blue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			color.b = blue;
			Assert.AreEqual(color, result);
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void SetColorGreenTest()
		{
			var colorProperty = new BlackboardPropertyName("property");
			var result = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var color = new Color(0.3f, 0.4f, 0.5f, 0.9f);
			const float green = 0.1f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorGreen, BlackboardPropertyName, float, BlackboardPropertyName>(colorProperty, green,
					result).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(result));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(result, out Color col));
			color.g = green;
			Assert.AreEqual(color, col);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetColorGreenVariableTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var greenProperty = new BlackboardPropertyName("green");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			
			var color = new Color(0.3f, 0.4f, 0.5f, 0.9f);
			const float green = 0.1f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorGreenVariable, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					colorProperty, greenProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(greenProperty, green);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			color.g = green;
			Assert.AreEqual(color, result);
			
			treeRoot.Dispose();
		}
		
		[Test]
		public static void SetColorRedTest()
		{
			var colorProperty = new BlackboardPropertyName("property");
			var result = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			var color = new Color(0.3f, 0.4f, 0.5f, 0.9f);
			const float red = 0.1f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorRed, BlackboardPropertyName, float, BlackboardPropertyName>(colorProperty, red,
					result).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(result));

			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(result, out Color col));
			color.r = red;
			Assert.AreEqual(color, col);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetColorRedVariableTest()
		{
			var colorProperty = new BlackboardPropertyName("color");
			var redProperty = new BlackboardPropertyName("red");
			var resultProperty = new BlackboardPropertyName("result");
			var blackboard = new Blackboard();
			
			var color = new Color(0.3f, 0.4f, 0.5f, 0.9f);
			const float red = 0.1f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorRedVariable, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					colorProperty, redProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();
			
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(colorProperty);
			blackboard.SetStructValue(redProperty, red);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));
			
			blackboard.SetStructValue(colorProperty, color);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			color.r = red;
			Assert.AreEqual(color, result);
			
			treeRoot.Dispose();
		}

		[Test]
		public static void SetColorVariableTest()
		{
			var redProperty = new BlackboardPropertyName("red");
			var greenProperty = new BlackboardPropertyName("green");
			var blueProperty = new BlackboardPropertyName("blue");
			var alphaProperty = new BlackboardPropertyName("alpha");
			var colorProperty = new BlackboardPropertyName("color");
			var blackboard = new Blackboard();

			const float red = 0.5f;
			const float green = 0.95f;
			const float blue = 0.3f;
			const float alpha = 0.8f;

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetColorVariable, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
					BlackboardPropertyName, BlackboardPropertyName>(redProperty, greenProperty, blueProperty,
					alphaProperty, colorProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.SetStructValue(redProperty, red);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(redProperty);
			blackboard.SetStructValue(greenProperty, green);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(greenProperty);
			blackboard.SetStructValue(blueProperty, blue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(blueProperty);
			blackboard.SetStructValue(alphaProperty, alpha);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(alphaProperty);
			blackboard.SetStructValue(redProperty, red);
			blackboard.SetStructValue(greenProperty, green);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(greenProperty);
			blackboard.SetStructValue(blueProperty, blue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(blueProperty);
			blackboard.SetStructValue(alphaProperty, alpha);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(alphaProperty);
			blackboard.SetStructValue(greenProperty, green);
			blackboard.SetStructValue(blueProperty, blue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(blueProperty);
			blackboard.SetStructValue(alphaProperty, alpha);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(greenProperty);
			blackboard.SetStructValue(blueProperty, blue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(redProperty);
			blackboard.RemoveStruct<float>(alphaProperty);
			blackboard.SetStructValue(greenProperty, green);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(blueProperty);
			blackboard.SetStructValue(alphaProperty, alpha);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.RemoveStruct<float>(greenProperty);
			blackboard.SetStructValue(blueProperty, blue);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(colorProperty));

			blackboard.SetStructValue(redProperty, red);
			blackboard.SetStructValue(greenProperty, green);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(colorProperty, out Color color));
			Assert.AreEqual(new Color(red, green, blue, alpha), color);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetCursorLockStateTest()
		{
			const CursorLockMode lockState = CursorLockMode.Confined;
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetCursorLockState, CursorLockMode>(lockState).Complete();
			TreeRoot treeRoot = treeBuilder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(lockState, Cursor.lockState);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetCursorLockStateVariableTest()
		{
			var lockStateProperty = new BlackboardPropertyName("lockState");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetCursorLockStateVariable, BlackboardPropertyName>(lockStateProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(lockStateProperty, CursorLockMode.None);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(CursorLockMode.None, Cursor.lockState);

			blackboard.SetStructValue(lockStateProperty, CursorLockMode.Locked);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(CursorLockMode.Locked, Cursor.lockState);

			blackboard.SetStructValue(lockStateProperty, CursorLockMode.Confined);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(CursorLockMode.Confined, Cursor.lockState);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetCursorVisibleTest()
		{
			const bool visible = false;
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetCursorVisible, bool>(visible).Complete();
			TreeRoot treeRoot = treeBuilder.Build();
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(visible, Cursor.visible);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetCursorVisibleVariableTest()
		{
			var visibleProperty = new BlackboardPropertyName("visible");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetCursorVisibleVariable, BlackboardPropertyName>(visibleProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetStructValue(visibleProperty, false);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(false, Cursor.visible);

			blackboard.SetStructValue(visibleProperty, true);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(true, Cursor.visible);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetGameObjectActiveTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetGameObjectActive, BlackboardPropertyName, bool>(goProperty, false).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var gameObject = new GameObject();
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(false, gameObject.activeSelf);

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetGameObjectActive, BlackboardPropertyName, bool>(goProperty, true).Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(true, gameObject.activeSelf);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetGameObjectActiveVariableTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var activeProperty = new BlackboardPropertyName("active");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetGameObjectActiveVariable, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					activeProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var gameObject = new GameObject();
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveObject<GameObject>(goProperty);
			blackboard.SetStructValue(activeProperty, false);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(false, gameObject.activeSelf);

			blackboard.SetStructValue(activeProperty, true);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(true, gameObject.activeSelf);

			treeRoot.Dispose();
		}
		
		[Test]
		public static void SetGameObjectLayerTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetGameObjectLayer, BlackboardPropertyName, int>(goProperty, 3).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var gameObject = new GameObject();
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(3, gameObject.layer);

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetGameObjectLayer, BlackboardPropertyName, int>(goProperty, 5).Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(5, gameObject.layer);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetGameObjectLayerVariableTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var layerProperty = new BlackboardPropertyName("layer");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetGameObjectLayerVariable, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					layerProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var gameObject = new GameObject();
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveObject<GameObject>(goProperty);
			blackboard.SetStructValue(layerProperty, 3);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(3, gameObject.layer);

			blackboard.SetStructValue(layerProperty, 5);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual(5, gameObject.layer);

			treeRoot.Dispose();
		}
		
		[Test]
		public static void SetGameObjectTagTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetGameObjectTag, BlackboardPropertyName, string>(goProperty, "Player").Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var gameObject = new GameObject();
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual("Player", gameObject.tag);

			treeRoot.Dispose();

			treeBuilder = new TreeBuilder();
			treeBuilder.AddLeaf<SetGameObjectTag, BlackboardPropertyName, string>(goProperty, "Finish").Complete();
			treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual("Finish", gameObject.tag);

			treeRoot.Dispose();
		}

		[Test]
		public static void SetGameObjectTagVariableTest()
		{
			var goProperty = new BlackboardPropertyName("go");
			var tagProperty = new BlackboardPropertyName("tag");
			var blackboard = new Blackboard();
			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SetGameObjectTagVariable, BlackboardPropertyName, BlackboardPropertyName>(goProperty,
					tagProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue<GameObject>(goProperty, null);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			var gameObject = new GameObject();
			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.RemoveObject<GameObject>(goProperty);
			blackboard.SetClassValue(tagProperty, "Player");
			Assert.AreEqual(Status.Error, treeRoot.Tick());

			blackboard.SetClassValue(goProperty, gameObject);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual("Player", gameObject.tag);

			blackboard.SetClassValue(tagProperty, "Finish");
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.AreEqual("Finish", gameObject.tag);

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

		[Test]
		public static void SubtractColorsTest()
		{
			var firstProperty = new BlackboardPropertyName("first");
			var secondProperty = new BlackboardPropertyName("second");
			var resultProperty = new BlackboardPropertyName("result");
			var first = new Color(0.9f, 0.8f, 0.7f, 0.75f);
			var second = new Color(0.6f, 0.5f, 0.4f, 0.3f);
			var blackboard = new Blackboard();

			var treeBuilder = new TreeBuilder();
			treeBuilder
				.AddLeaf<SubtractColors, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>(
					firstProperty, secondProperty, resultProperty).Complete();
			TreeRoot treeRoot = treeBuilder.Build(blackboard);
			treeRoot.Initialize();

			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.RemoveStruct<Color>(firstProperty);
			blackboard.SetStructValue(secondProperty, second);
			Assert.AreEqual(Status.Error, treeRoot.Tick());
			Assert.IsFalse(blackboard.ContainsStructValue<Color>(resultProperty));

			blackboard.SetStructValue(firstProperty, first);
			Assert.AreEqual(Status.Success, treeRoot.Tick());
			Assert.IsTrue(blackboard.TryGetStructValue(resultProperty, out Color result));
			Assert.AreEqual(first - second, result);

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
