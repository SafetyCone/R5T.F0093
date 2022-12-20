using System;

using R5T.F0042.N000;
using R5T.F0087;
using R5T.T0142;


namespace R5T.F0093
{
    [DataTypeMarker]
    public class CreateConsoleRepositoryResult : CreateRepositoryResultBase
    {
        public RepositoryResult RepositoryResult { get; set; }
        public ConsoleSolutionResult ConsoleSolutionResult { get; set; }

        public override string SolutionFilePath => this.ConsoleSolutionResult.SolutionContext.SolutionFilePath;
    }
}
