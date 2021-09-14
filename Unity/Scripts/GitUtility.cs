#if UNITY_GITSNAP
using UnityEngine;

using System;
using System.Diagnostics;
using System.IO;

namespace GitSnaphot
{
	/// <summary>
	/// A utility for creating a snapshot of the current state of the repository.
	/// </summary>
	public class GitUtility : MonoBehaviour
	{
		#region CONSTANTS
		public const string SNAPSHOT_SUBPATH = "Assets/Resources/GitSnapshot";
		public const string SNAPSHOT_FILENAME = "git-snapshot.json";
		#endregion


		#region PROPERTIES
		public static string snapshotPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), SNAPSHOT_SUBPATH, SNAPSHOT_FILENAME);
		#endregion


		#region PUBLIC API
		public static void CreateSnapshot()
		{
			Log("Creating snapshot of Git repository state...");

			Snapshot();
		}

		public static GitSnapshot LoadSnapshot()
		{
			if (Directory.Exists(SNAPSHOT_SUBPATH) && File.Exists(snapshotPath))
			{
				var json = File.ReadAllText(snapshotPath);
				return JsonUtility.FromJson<GitSnapshot>(json);
			}
			else
			{
				LogError($"No Git snapshot exists at directory: {snapshotPath}");
				return null;
			}
		}

		public static bool SaveSnapshot(GitSnapshot snapshot)
		{
			try
			{
				if (!Directory.Exists(SNAPSHOT_SUBPATH))
				{
					Directory.CreateDirectory(SNAPSHOT_SUBPATH);
				}

				var json = JsonUtility.ToJson(snapshot, true);
				File.WriteAllText(snapshotPath, json);
				return true;
			}
			catch (Exception e)
			{
				LogError($"Exception occured while saving snapshot:\n{e}");
				return false;
			}
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
					WorkingDirectory = Directory.GetCurrentDirectory()
				}
			};

			process.Start();

			string line = process.StandardOutput.ReadToEnd();
			string err = process.StandardError.ReadToEnd();

			Log(line);
			LogError(err);

			process.WaitForExit();
			process.Close();

			// TODO Write result to storage.
			var snap = new GitSnapshot();
			SaveSnapshot(snap);
		}
		#endregion


		#region LOGGING
		static void Log(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				UnityEngine.Debug.Log(CreateLog(text, "orange"));
			}
		}

		static void LogError(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				UnityEngine.Debug.LogError(CreateLog($"{text}", "red"));
				UnityEngine.Debug.LogError(CreateLog($"Current dir: {Directory.GetCurrentDirectory()}", "red"));
			}
		}

		static string CreateLog(string text, string color)
				=> $"<color={color}>[{nameof(GitUtility)}]</color> {text}";
		#endregion
	}
}
#endif