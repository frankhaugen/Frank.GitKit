using Octokit;

namespace Frank.GitKit;

public interface IRemoteGitProvider : IGitProvider
{
    Task CreateRepositoryAsync(string repositoryName, string description, string homepage = "");
    Task CreatePullRequestAsync(string sourceBranch, string targetBranch, string title, string description);
    
}