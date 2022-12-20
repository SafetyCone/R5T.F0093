using System;

using R5T.F0080;


namespace R5T.F0093
{
    public class CreateNonSolutionedRepositoryResult : IHasRemoteAndLocalRepositoryResult
    {
        public RemoteRepositoryResult RemoteRepositoryResult { get; set; }
        public F0090.LocalRepositoryResult LocalRepositoryResult { get; set; }
    }
}
