using System;


namespace R5T.F0093
{
    public class RepositorySetupOperations : IRepositorySetupOperations
    {
        #region Infrastructure

        public static IRepositorySetupOperations Instance { get; } = new RepositorySetupOperations();


        private RepositorySetupOperations()
        {
        }

        #endregion
    }
}
