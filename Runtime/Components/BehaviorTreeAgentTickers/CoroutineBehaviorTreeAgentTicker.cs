// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	public abstract class CoroutineBehaviorTreeAgentTicker : BehaviorTreeAgentTicker
	{
		private YieldInstruction m_instruction;
		private Coroutine m_process;

		[CanBeNull]
		protected abstract YieldInstruction instruction { get; }

		[MethodImpl(MethodImplOptions.AggressiveInlining), ContextMenu("Update Instruction")]
		protected void UpdateInstruction()
		{
			m_instruction = instruction;
		}

		protected override void OnValidate()
		{
			m_instruction = instruction;
		}

		private void Awake()
		{
			m_instruction = instruction;
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			if (!enabled)
			{
				return;
			}

			m_process = StartCoroutine(Process());
		}

		private void OnDisable()
		{
			if (m_process != null)
			{
				StopCoroutine(m_process);
				m_process = null;
			}
		}

		private IEnumerator Process()
		{
			while (true)
			{
				yield return m_instruction;
				Tick();
			}
		}
	}
}
