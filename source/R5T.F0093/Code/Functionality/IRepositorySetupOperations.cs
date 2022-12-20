using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.F0090;
using R5T.T0132;
using R5T.T0153;

using RepositoryContext = R5T.T0153.N005.RepositoryContext;


namespace R5T.F0093
{
    [FunctionalityMarker]
    public partial interface IRepositorySetupOperations : IFunctionalityMarker
    {
        /// <summary>
        /// The standard actions are:
        /// <list type="number">
        /// <item>Before, create the remote and local repositories.</item>
        /// <item>After, perform an initial checkin.</item>
        /// </list>
        /// </summary>
        public IEnumerable<Func<RepositoryContext, Task>> SetupRepository_WithStandardActions<TRepositoryResult>(
            TRepositoryResult repositoryResult,
            IEnumerable<Func<RepositoryContext, Task>> setupRepositoryActions)
            where TRepositoryResult : IHasRemoteAndLocalRepositoryResult
        {
            var output = EnumerableOperator.Instance.From(
                RepositoryOperations.Instance.CreateRemoteAndLocalRepositories(repositoryResult))
                .AppendRange(setupRepositoryActions)
                .Append(
                    RepositoryOperations.Instance.InitialCheckin)
                ;

            return output;
        }

        /// <inheritdoc cref="SetupRepository_WithStandardActions{TRepositoryResult}(TRepositoryResult, IEnumerable{Func{RepositoryContext, Task}})"/>
        public IEnumerable<Func<RepositoryContext, Task>> SetupRepository_WithStandardActions<TRepositoryResult>(
            TRepositoryResult repositoryResult,
            params Func<RepositoryContext, Task>[] setupRepositoryActions)
            where TRepositoryResult : IHasRemoteAndLocalRepositoryResult
        {
            var output = this.SetupRepository_WithStandardActions(
                repositoryResult,
                setupRepositoryActions.AsEnumerable());

            return output;
        }
    }
}
