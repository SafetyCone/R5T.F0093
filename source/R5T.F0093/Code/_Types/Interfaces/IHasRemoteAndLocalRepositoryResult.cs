using System;

using R5T.F0080;


namespace R5T.F0093
{
    public interface IHasRemoteAndLocalRepositoryResult : F0090.IHasLocalRepositoryResult
    {
        RemoteRepositoryResult RemoteRepositoryResult { get; set; }
    }
}
