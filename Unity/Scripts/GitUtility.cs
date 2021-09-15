#if UNITY_GITSNAP
using UnityEngine;

using System;
using System.Diagnostics;
using System.IO;

using Debug = UnityEngine.Debug;

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
		public static bool verboseLogging { get; set; } = false;
		#endregion


		#region PUBLIC API
		public static void CreateSnapshot()
		{
			Log("Creating snapshot of Git repository state...", alwaysLog: true);

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

		public static bool Execute(string cmd, string args, ref string output)
		{

			var process = new Process {

				StartInfo = new ProcessStartInfo {

					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true,

					FileName = cmd,
					Arguments = args,
					WorkingDirectory = Directory.GetCurrentDirectory()
				}
			};

			process.Start();

			string line = process.StandardOutput.ReadToEnd();
			if (!string.IsNullOrEmpty(line))
			{
				Log($"{cmd} {args} > {line}");
			}

			string err = process.StandardError.ReadToEnd();
			if (!string.IsNullOrEmpty(err))
			{
				LogError($"{cmd} {args} > {err}");
			}

			process.WaitForExit();
			process.Close();

			bool success = string.IsNullOrEmpty(err);
			output = (success ? line : err).TrimEnd('\n');
			return success;
		}
		#endregion


		#region HELPER FUNCTIONS
		static void Snapshot()
		{
			var snap = new GitSnapshot();

			Execute("git", "rev-parse HEAD", ref snap.commitHashLong);
			Execute("git", "rev-parse --short HEAD", ref snap.commitHashShort);
			Execute("git", "describe", ref snap.relativeTag);

			SaveSnapshot(snap);
		}
		#endregion


		#region LOGGING
		static void Log(string text, bool alwaysLog = false)
		{
			if (verboseLogging || alwaysLog)
			{
				Debug.Log(CreateLog(text, "orange"));
			}
		}

		static void LogError(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				Debug.LogError(CreateLog(text, "red"));
				Debug.LogError(CreateLog($"Current dir: {Directory.GetCurrentDirectory()}", "red"));
			}
		}

		static string CreateLog(string text, string color)
			=> $"<color={color}>[{nameof(GitUtility)}]</color> {text}";
		#endregion
	}
}
#endif