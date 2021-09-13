#if UNITY_GITUTIL
namespace GitCommitReader
{
	/// <summary>
	/// Provides an API for retrieving information about the current state of the repository.
	/// </summary>
	public static class GitInterface
	{
		#region PUBLIC API
		public static GitSnapshot GetSnapshot()
			=> GitUtility.LoadSnapshot();
		#endregion
	}
}
#endif