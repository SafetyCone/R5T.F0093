using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.F0087;
using R5T.F0090;
using R5T.T0132;
using R5T.T0153;

using RepositoryContext = R5T.T0153.N005.RepositoryContext;


namespace R5T.F0093
{
	[FunctionalityMarker]
	public partial interface IRepositoryOperator : IFunctionalityMarker,
		F0042.IRepositoryOperator
	{
        /// <summary>
        /// Creates the remote (GitHub) repository and clones it to a local directory to create the local repository.
        /// </summary>
        public async Task CreateRemoteAndLocalRepositories<TRepositoryResult>(
			RepositoryContext repositoryContext,
			TRepositoryResult repositoryResult)
			where TRepositoryResult : IHasRemoteAndLocalRepositoryResult
        {
            var remoteAndLocalRepositoryResult = await F0080.RepositoryOperations.Instance.CreateRepository(
                repositoryContext.RepositoryOwner,
                repositoryContext.RepositoryName,
                repositoryContext.RepositoryDescription,
                repositoryContext.IsPrivate);

            repositoryResult.LocalRepositoryResult = new LocalRepositoryResult
            {
                DirectoryPath = remoteAndLocalRepositoryResult.LocalRepositoryDirectoryPath,
            };

            repositoryResult.RemoteRepositoryResult = new F0080.RemoteRepositoryResult
            {
                RepositoryUrl = remoteAndLocalRepositoryResult.RemoteRepositoryUrl,
            };
        }

  //      public async Task CreateRepository(
		//	RepositoryContext repositoryContext,
		//	IEnumerable<Func<RepositoryContext, Task>> repositoryActions)
		//{
  //          await ActionOperator.Instance.Run(
  //              repositoryContext,
  //              repositoryActions);
		//}

  //      public Task CreateRepository(
  //          LibraryContext libraryContext,
  //          string ownerName,
  //          bool isPrivate,
  //          IEnumerable<Func<RepositoryContext, Task>> repositoryActions)
  //      {
  //          var repositoryContext = Instances.RepositoryContextOperations.GetRepositoryContext(
  //              ownerName,
  //              libraryContext,
  //              isPrivate);

  //          return this.CreateRepository(
  //              repositoryContext,
  //              repositoryActions);
  //      }

  //      public Task CreateRepository(
  //          LibraryContext libraryContext,
  //          string ownerName,
  //          bool isPrivate,
  //          params Func<RepositoryContext, Task>[] repositoryActions)
  //      {
  //          var repositoryContext = Instances.RepositoryContextOperations.GetRepositoryContext(
  //              ownerName,
  //              libraryContext,
  //              isPrivate);

  //          return this.CreateRepository(
  //              repositoryContext,
  //              repositoryActions.AsEnumerable());
  //      }
    }
}