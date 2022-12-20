using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.F0042;
using R5T.F0042.N000;
using R5T.F0089;
using R5T.T0132;
using R5T.T0153;

using N000 = R5T.F0042.N000;

using RepositoryContext = R5T.T0153.N005.RepositoryContext;


namespace R5T.F0093
{
	[FunctionalityMarker]
	public partial interface IRepositoryOperations : IFunctionalityMarker,
        F0080.IRepositoryOperations
	{
        /// <summary>
        /// Creates a repository containing only the standard gitignore file.
        /// </summary>
        public async Task<CreateNonSolutionedRepositoryResult> NewRepository_GitIgnoreOnly(
            LibraryContext libraryContext,
            string ownerName,
            bool isPrivate,
            ILogger logger)
        {
            var repositoryResult = new CreateNonSolutionedRepositoryResult();

            await F0090.RepositoryOperator.Instance.CreateRepository(
                libraryContext,
                ownerName,
                isPrivate,
                RepositorySetupOperations.Instance.SetupRepository_WithStandardActions(
                    repositoryResult,
                    F0090.RepositorySetupOperations.Instance.SetupRepository_GitIgnoreOnly(
                        libraryContext)));

            return repositoryResult;
        }

        public async Task<ConsoleRepositoryResult> NewRepository_Console(
            LibraryContext libraryContext,
            string ownerName,
            bool isPrivate,
            ILogger logger)
        {
            var repositoryResult = new ConsoleRepositoryResult();

            await F0090.RepositoryOperator.Instance.CreateRepository(
                libraryContext,
                ownerName,
                isPrivate,
                RepositorySetupOperations.Instance.SetupRepository_WithStandardActions(
                    repositoryResult,
                    F0090.RepositorySetupOperations.Instance.SetupRepository_Console(
                        libraryContext,
                        repositoryResult)));

            return repositoryResult;
        }

        public Func<RepositoryContext, Task> CreateRemoteAndLocalRepositories<TRepositoryResult>(
            TRepositoryResult repositoryResult)
            where TRepositoryResult : IHasRemoteAndLocalRepositoryResult
        {
            return repositoryContext => RepositoryOperator.Instance.CreateRemoteAndLocalRepositories(
                repositoryContext,
                repositoryResult);
        }

        public Task InitialCheckin(RepositoryContext repositoryContext)
        {
            F0080.RepositoryOperations.Instance.Checkin(
                repositoryContext.LocalDirectoryPath,
                CommitMessages.Instance.InitialCommit);

            return Task.CompletedTask;
        }

        public async Task<CreateRepositoryResult> CreateRepository_WinFormsApplication(
            string repositoryName,
            string repositoryDescription,
            string owner,
            bool isPrivate,
            ILogger logger)
        {
            var libraryContext = LibraryContextOperations.Instance.Create(
                repositoryName,
                repositoryDescription);

            var repositoryContext = Instances.RepositoryContextOperations.GetRepositoryContext(
                owner,
                libraryContext,
                isPrivate);

            var solutionRepositoryResult = await this.CreateRepository(
                libraryContext,
                repositoryContext,
                async repositoryResult =>
                {
                    var solutionResult = await F0087.SolutionOperations.Instance.NewSolution_WindowsFormsApplication(
                        libraryContext,
                        repositoryContext.IsPrivate,
                        repositoryContext.LocalDirectoryPath);

                    var consoleRepositoryResult = new CreateRepositoryResult
                    {
                        LocalRepositoryResult = repositoryResult,
                        SolutionResult = solutionResult,
                    };

                    return consoleRepositoryResult;
                },
                logger);

            return solutionRepositoryResult;
        }

        public async Task<TCreateRepositoryResult> CreateRepository<TCreateRepositoryResult>(
            string owner,
            LibraryContext libraryContext,
            bool isPrivate,
            Func<RepositoryResult, Task<TCreateRepositoryResult>> createSolutionOperation,
            ILogger logger)
            where TCreateRepositoryResult : CreateRepositoryResultBase
        {
            var repositoryContext = Instances.RepositoryContextOperations.GetRepositoryContext(
                owner,
                libraryContext,
                isPrivate);

            var repositoryResult = await this.CreateRepository(
                libraryContext,
                repositoryContext,
                createSolutionOperation,
                logger);

            return repositoryResult;
        }

        /// <inheritdoc cref="Documentation.CreateRepository"/>
        public async Task<TCreateRepositoryResult> CreateRepository<TCreateRepositoryResult>(
            LibraryContext libraryContext,
            RepositoryContext repositoryContext,
            Func<RepositoryResult, Task<TCreateRepositoryResult>> createSolutionOperation,
            ILogger logger)
            where TCreateRepositoryResult : CreateRepositoryResultBase
        {
            logger.LogInformation("SAFETY CHECK: Verifying repository does not already exist...");

            await this.Verify_RepositoryDoesNotExist(
                repositoryContext);

            logger.LogInformation("Creating repository...");

            TCreateRepositoryResult repositoryResult = default;

            async Task SetupRepositoryAction()
            {
                repositoryResult = await this.SetupRepository(
                    libraryContext,
                    repositoryContext,
                    createSolutionOperation,
                    logger);
            }

            var remoteRepositoryUrl = await this.CreateRepository(
                repositoryContext,
                SetupRepositoryAction);

            repositoryResult.RemoteRepositoryUrl = remoteRepositoryUrl;
            repositoryResult.LocalDirectoryPath = repositoryContext.LocalDirectoryPath;

            return repositoryResult;
        }

        public async Task<CreateConsoleRepositoryResult> CreateRepository_Console(
            string owner,
            LibraryContext libraryContext,
            bool isPrivate,
            ILogger logger)
        {
            var repositoryContext = Instances.RepositoryContextOperations.GetRepositoryContext(
                owner,
                libraryContext,
                isPrivate);

            var consoleRepositoryResult = await this.CreateRepository_Console_NoSafetyCheck(
                libraryContext,
                repositoryContext,
                logger);

            return consoleRepositoryResult;
        }

        public async Task<CreateConsoleRepositoryResult> CreateRepository_Console_NoSafetyCheck(
            LibraryContext libraryContext,
            RepositoryContext repositoryContext,
            ILogger logger)
        {
            CreateConsoleRepositoryResult consoleRepositoryResult = default;

            async Task SetupRepositoryAction()
            {
                consoleRepositoryResult = await this.SetupRepository_Console(
                    libraryContext,
                    repositoryContext,
                    logger);
            }

            await this.CreateRepository(
                repositoryContext,
                SetupRepositoryAction);

            return consoleRepositoryResult;
        }

        public async Task<CreateConsoleRepositoryResult> CreateRepository_Console(
            LibraryContext libraryContext,
            RepositoryContext repositoryContext,
            ILogger logger)
        {
            logger.LogInformation("SAFETY CHECK: Verifying repository does not already exist...");

            await this.Verify_RepositoryDoesNotExist(
                repositoryContext);

            logger.LogInformation("Creating console repository...");

            var consoleRepositoryResult = await this.CreateRepository_Console_NoSafetyCheck(
                libraryContext,
                repositoryContext,
                logger);

            return consoleRepositoryResult;
        }

        public async Task<TRepositoryResult> SetupRepository<TRepositoryResult>(
            LibraryContext libraryContext,
            RepositoryContext repositoryContext,
            Func<RepositoryResult, Task<TRepositoryResult>> createSolutionOperation,
            ILogger logger)
        {
            var repositoryResult = this.SetupRepository(repositoryContext, logger);

            var result = await createSolutionOperation(repositoryResult);
            return result;
        }

        public async Task<CreateConsoleRepositoryResult> SetupRepository_Console(
            LibraryContext libraryContext,
            RepositoryContext repositoryContext,
            ILogger logger)
        {
            var repositoryResult = this.SetupRepository(repositoryContext, logger);

            var consoleSolutionResult = await Instances.SolutionOperations.NewSolution_Console(
                libraryContext,
                repositoryContext.IsPrivate,
                repositoryContext.LocalDirectoryPath);

            var consoleRepositoryResult = new CreateConsoleRepositoryResult
            {
                RepositoryResult = repositoryResult,
                ConsoleSolutionResult = consoleSolutionResult,
            };

            return consoleRepositoryResult;
        }

        public N000.RepositoryResult SetupRepository(
            RepositoryContext repositoryContext,
            ILogger logger)
        {
            var repositoryResult = RepositoryOperator.Instance.SetupRepository(
                repositoryContext.LocalDirectoryPath,
                logger);

            return repositoryResult;
        }
    }
}