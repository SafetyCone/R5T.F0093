using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

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
        public async Task<ConsoleRepositoryResult> CreateRepository_Console(
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

        public async Task<ConsoleRepositoryResult> CreateRepository_Console_NoSafetyCheck(
            LibraryContext libraryContext,
            RepositoryContext repositoryContext,
            ILogger logger)
        {
            ConsoleRepositoryResult consoleRepositoryResult = default;

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

        public async Task<ConsoleRepositoryResult> CreateRepository_Console(
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

        public async Task<ConsoleRepositoryResult> SetupRepository_Console(
            LibraryContext libraryContext,
            RepositoryContext repositoryContext,
            ILogger logger)
        {
            var repositoryResult = this.SetupRepository(repositoryContext, logger);

            var consoleSolutionResult = await Instances.SolutionOperations.CreateSolution_Console(
                libraryContext,
                repositoryContext.IsPrivate,
                repositoryContext.LocalDirectoryPath);

            var consoleRepositoryResult = new ConsoleRepositoryResult
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