#if UNITY_GITSNAP
namespace GitSnaphot
{
	/// <summary>
	/// Provides an API for retrieving information about the current state of the repository.
	/// </summary>
	public static class GitInterface
	{
		#region PUBLIC API
		/// <summary>
        /// Retrieves the snapshot from storage.
        /// </summary>
        /// <returns>The snapshot data model.</returns>
		public static GitSnapshot GetSnapshot()
			=> GitUtility.LoadSnapshot();
		#endregion
	}
}
#endif