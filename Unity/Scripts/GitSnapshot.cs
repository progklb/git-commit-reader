#if UNITY_GITUTIL
namespace GitCommitReader
{
	public class GitSnapshot
	{
		// TODO Verify that this terminology is correct.
		// TODO Summarise.
		public string commitHashShort;
		public string commitHashLong;
		public string relativeTag;
	}
}
#endif