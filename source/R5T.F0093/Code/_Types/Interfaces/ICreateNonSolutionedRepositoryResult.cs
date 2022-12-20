using System;


namespace R5T.F0093
{
    public interface ICreateNonSolutionedRepositoryResult
    {
        string RemoteRepositoryUrl { get; }
        string LocalDirectoryPath { get; }
    }
}
