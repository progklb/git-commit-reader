#if UNITY_GITUTIL
using UnityEditor;

namespace GitCommitReader
{
	/// <summary>
	/// Defines a checkmark menu item for controlling whether automatic updates occur on recompilation.
	/// </summary>
	[InitializeOnLoad]
	public static class AutoUpdateCheckmarkMenuItem
	{
		public const string MENU_NAME = "Tools/Git Reader/Auto-Update on Compile";

		public static bool isEnabled { get; set; }

		/// Called on load thanks to the InitializeOnLoad attribute
		static AutoUpdateCheckmarkMenuItem()
		{
			isEnabled = EditorPrefs.GetBool(MENU_NAME, false);
			EditorApplication.delayCall += () => PerformAction(isEnabled);
		}

		public static void PerformAction(bool enabled)
		{
			Menu.SetChecked(MENU_NAME, enabled);
			EditorPrefs.SetBool(MENU_NAME, enabled);
			isEnabled = enabled;
		}
	}

	/// <summary>
	/// Exposes menu items in the Unity Editor.
	/// </summary>
	public class GitReaderMenu
	{
		[MenuItem("Tools/Git Reader/Update Git Info")] 
		public static void UpdateGitInfo()
		{
			GitUtility.CreateSnapshot();
		}

		[MenuItem(AutoUpdateCheckmarkMenuItem.MENU_NAME)] 
		public static void AutoUpdateOnCompile()
		{
			AutoUpdateCheckmarkMenuItem.PerformAction(!AutoUpdateCheckmarkMenuItem.isEnabled);
		}
	}
}
#endif