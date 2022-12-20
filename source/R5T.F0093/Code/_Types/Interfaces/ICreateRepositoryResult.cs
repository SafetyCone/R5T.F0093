using System;

using R5T.T0142;


namespace R5T.F0093
{
    [DataTypeMarker]
    public interface ICreateRepositoryResult : ICreateNonSolutionedRepositoryResult
    {
        string SolutionFilePath { get; }
    }
}
