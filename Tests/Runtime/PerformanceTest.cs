using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.StatusBehaviors;

namespace Zor.BehaviorTree.Tests
{
	public class PerformanceTest : MonoBehaviour
	{
		private Zor.BehaviorTree.Core.Tree m_tree;

		private void Start()
		{
			var builder = new TreeBuilder();
			builder.AddBehavior<Parallel>(Parallel.Mode.All);
			for (int i = 0; i < 999999; ++i)
			{
				builder.AddBehavior<RunningBehavior>().Finish();
			}
			builder.Finish();

			m_tree = builder.Build();
			m_tree.Initialize();
		}

		private void Update()
		{
			m_tree.Tick();
		}

		private void OnDestroy()
		{
			m_tree.Dispose();
		}
	}
}
