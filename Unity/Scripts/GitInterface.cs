#if UNITY_GITSNAP
namespace GitSnaphot
{
	/// <summary>
	/// Provides an API for retrieving information about the current state of the repository.
	/// </summary>
	public static class GitInterface
	{
		#region PUBLIC API
		public static GitSnapshot GetSnapshot()
			=> GitUtility.LoadSnapshot();

		// TODO Expose each possible git command here as a separate method?
		// GitUtility to expose the process/output as a public method.
		#endregion
	}
}
#endif