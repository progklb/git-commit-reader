#if UNITY_GITUTIL
using UnityEditor;
using UnityEditor.Callbacks;

namespace GitCommitReader
{
	/// <summary>
	/// Exposes menu items in the Unity Editor.
	/// </summary>
	public class Menu
	{
		[MenuItem("Tools/Update Git Info")]
		public static void UpdateGitInfo()
		{
			GitUtility.StartSnapshot();
		}
	}

	/// <summary>
	/// Is invoked on Unity builds to ensure that the latest repository state is stored.
	/// </summary>
	public class BuildPostProcessor
	{
		[PostProcessBuild]
		public static void ReadWriteCommitInfo()
		{
			GitUtility.StartSnapshot();
		}
	}
}
#endif