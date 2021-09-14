#if UNITY_GITUTIL
using UnityEditor;
using UnityEditor.Callbacks;

namespace GitCommitReader
{
	/// <summary>
	/// A set of processors for automating behavior.
	/// </summary>
	public class Processors
	{
		/// <summary>
		/// Is invoked on compilation.
		/// Will create a snapshot of the repository state if the menu item is checked.
		/// </summary>
		[DidReloadScripts]
		private static void OnScriptsReloaded()
		{
			if (EditorPrefs.GetBool(AutoUpdateCheckmarkMenuItem.MENU_NAME))
			{ 
				GitUtility.CreateSnapshot();
			}
		}

		/// <summary>
		/// Is invoked on Unity builds to ensure that the latest repository state is stored.
		/// </summary>
		[PostProcessBuild]
		public static void OnPostProcessBuild()
		{
			GitUtility.CreateSnapshot();
		}
	}
}
#endif