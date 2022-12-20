using System;

using R5T.F0080;
using R5T.F0087;
using R5T.T0142;


namespace R5T.F0093
{
    [DataTypeMarker]
    public class SolutionRepositoryResult<TSolutionResult> : F0090.SolutionRepositoryResult<TSolutionResult>,
        IHasRemoteAndLocalRepositoryResult
    {
        public RemoteRepositoryResult RemoteRepositoryResult { get; set; }
    }

    [DataTypeMarker]
    public class SolutionRepositoryResult : SolutionRepositoryResult<SolutionResult>
    {
    }
}
