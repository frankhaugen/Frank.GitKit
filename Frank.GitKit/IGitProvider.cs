using LibGit2Sharp;
using Microsoft.Extensions.Options;

namespace Frank.GitKit;

public interface IGitProvider
{
    Task CloneRepositoryAsync(Uri remoteRepository, DirectoryInfo localDirectory, string branch = "main");
    Task CommitAsync(string message);
    Task PushAsync();
    Task<IEnumerable<string>> GetBranchesAsync();
    
    Task CreateBranchAsync(string branchName, bool checkout = true);
    
    Task CheckoutBranchAsync(string branchName, bool create = true);
}

/// <summary>
/// Represents a basic git provider with no authentication. (e.g. public GitHub, GitLab, Bitbucket repositories or local git repositories)
/// </summary>
public class BasicGitProvider : IGitProvider
{
    /// <inheritdoc />
    public async Task CloneRepositoryAsync(Uri remoteRepository, DirectoryInfo localDirectory, string branch = "main")
    {
        var options = new CloneOptions
        {
            BranchName = branch,
            Checkout = true,
        };
        Repository.Clone(remoteRepository.ToString(), localDirectory.FullName, options);
    }

    /// <inheritdoc />
    public async Task CommitAsync(string message)
    {
        
    }

    /// <inheritdoc />
    public async Task PushAsync()
    {
    }

    /// <inheritdoc />
    public async Task<IEnumerable<string>> GetBranchesAsync()
    {
        return null;
    }

    /// <inheritdoc />
    public async Task CreateBranchAsync(string branchName, bool checkout = true)
    {
    }

    /// <inheritdoc />
    public async Task CheckoutBranchAsync(string branchName, bool create = true)
    {
    }
}