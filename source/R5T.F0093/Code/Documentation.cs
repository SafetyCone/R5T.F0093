using System;


namespace R5T.F0093
{
	/// <summary>
	/// More repository operations (complex).
	/// </summary>
	public static class Documentation
	{
        /// <summary>
        /// Perfors a safety check that the repository does not already exist, then creates a remote repository, clones it to a local directory path, runs the provided solution creation action, then does a checkin of the local repository.
        /// </summary>
        public static readonly object CreateRepository;
	}
}