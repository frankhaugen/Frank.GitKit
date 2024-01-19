namespace Frank.GitKit;

public interface ILocalGitProvider : IGitProvider
{
    Task CreateBranchAsync(string branchName);
    Task MergeBranchAsync(string sourceBranch, string targetBranch);
    Task DeleteBranchAsync(string branchName);
    Task<IEnumerable<string>> GetLocalBranchesAsync();
    
}