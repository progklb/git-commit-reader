#if UNITY_GITSNAP
namespace GitSnaphot
{
	/// <summary>
	/// Represents the current state of a Git repository.
	/// </summary>
	public class GitSnapshot
	{
		/// <summary>
		/// The full commit hash of the current Git commit.
		/// </summary>
		public string commitHashShort;
		/// <summary>
		/// The short commit hash of the current Git commit.
		/// </summary>
		public string commitHashLong;
		/// <summary>
		/// The current Git commit, described by the nearest tag (with offset, if necessary).
		/// </summary>
		public string relativeTag;
	}
}
#endif