using System;


namespace R5T.F0093
{
    public abstract class CreateRepositoryResultBase : ICreateRepositoryResult
    {
        public string RemoteRepositoryUrl { get; set; }
        public string LocalDirectoryPath { get; set; }

        public abstract string SolutionFilePath { get; }
    }
}
