#if UNITY_GITUTIL
using System;
using System.Diagnostics;
using UnityEngine;

namespace GitCommitReader
{
	/// <summary>
	/// A utility for creating a snapshot of the current state of the repository.
	/// </summary>
	public class GitUtility : MonoBehaviour
	{
		#region PUBLIC API
		public static void StartSnapshot()
		{
			Log("Creating snapshot of Git repository state...");

			// TODO Capture all values and write to JSON file / scriptable object.
			Snapshot();
		}
		#endregion


		#region HELPER FUNCTIONS
		static void Snapshot()
		{
			var process = new Process {

				StartInfo = new ProcessStartInfo {
					
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true,

					FileName = "git",
					// TODO Allow for passing in different commands and awaiting/actioning their result.
					Arguments = "rev-parse HEAD",
					// TODO Figure out how to determine/inject a directory.
					WorkingDirectory = @"E:\My Work and Projects\Programming\git-commit-reader-unity\Assets\Submodules\git-commit-reader"
				}
			};

			process.Start();

			string line = process.StandardOutput.ReadToEnd();
			string err = process.StandardError.ReadToEnd();
			Log(line);
			LogError(err);

			process.WaitForExit();
			process.Close();
		}

		static void Log(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				UnityEngine.Debug.Log($"<color=orange>[{nameof(GitUtility)}] {text}</color>");
			}
		}

		static void LogError(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				UnityEngine.Debug.LogError($"<color=red>[{nameof(GitUtility)}] {text}</color>");
			}
		}
		#endregion
	}
}
#endif