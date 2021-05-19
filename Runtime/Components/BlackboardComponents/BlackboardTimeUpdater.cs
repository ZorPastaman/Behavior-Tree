// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.SimpleBlackboard.Components;
using Zor.SimpleBlackboard.Components.Accessors;

namespace Zor.BehaviorTree.Components.BlackboardComponents
{
	[AddComponentMenu(AddComponentConstants.SimpleBlackboardFolder + "Updaters/Blackboard Time Updater")]
	public sealed class BlackboardTimeUpdater : MonoBehaviour
	{
		[SerializeField] private FloatAccessor m_TimeAccessor;

		private void Update()
		{
			m_TimeAccessor.value = Time.time;
		}
	}
}
