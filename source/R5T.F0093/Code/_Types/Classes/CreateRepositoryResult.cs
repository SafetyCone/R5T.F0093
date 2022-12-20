using System;

using R5T.F0042.N000;
using R5T.F0087;
using R5T.T0142;


namespace R5T.F0093
{
    [DataTypeMarker]
    public class CreateRepositoryResult : CreateRepositoryResultBase
    {
        public RepositoryResult LocalRepositoryResult { get; set; }
        public SolutionResult SolutionResult { get; set; }

        public override string SolutionFilePath => this.SolutionResult.SolutionContext.SolutionFilePath;
    }
}
