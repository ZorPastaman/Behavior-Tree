// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.SimpleBlackboard.Components;
using Zor.SimpleBlackboard.Components.Accessors;

namespace Zor.BehaviorTree.Components.BlackboardComponents
{
	[AddComponentMenu(AddComponentConstants.SimpleBlackboardFolder + "Updaters/Blackboard Frame Updater")]
	public sealed class BlackboardFrameUpdater : MonoBehaviour
	{
		[SerializeField] private IntAccessor m_FrameAccessor;

		private void Update()
		{
			m_FrameAccessor.value = Time.frameCount;
		}
	}
}
