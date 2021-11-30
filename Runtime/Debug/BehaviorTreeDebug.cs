using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Zor.BehaviorTree.Debugging
{
	public static class BehaviorTreeDebug
	{
		public const string LogDefine = "BEHAVIOR_TREE_LOG";
		public const string WarningDefine = "BEHAVIOR_TREE_WARNING";
		public const string ErrorDefine = "BEHAVIOR_TREE_ERROR";

		private const string Format = "[BehaviorTree] {0}.";

		[Conditional(LogDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Log(string message)
		{
			Debug.LogFormat(Format, message);
		}

		[Conditional(WarningDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogWarning(string message)
		{
			Debug.LogWarningFormat(Format, message);
		}

		[Conditional(WarningDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogWarning(Object context, string message)
		{
			Debug.LogWarningFormat(context, Format, message);
		}

		[Conditional(ErrorDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogError(string message)
		{
			Debug.LogErrorFormat(Format, message);
		}

		[Conditional(ErrorDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogError(Object context, string message)
		{
			Debug.LogErrorFormat(context, Format, message);
		}
	}
}
