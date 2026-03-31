using LibGit2Sharp;
using Octokit;
using Repository = LibGit2Sharp.Repository;

namespace Frank.GitKit.Providers.GitHub;

public class GitHubProvider : IGitProvider
{
    private readonly GitHubClient _client;

public GitHubProvider(GitHubClient client)
    {
        _client = client;
    }
    
    public async Task CreateRepositoryAsync(string repositoryName, string description = "", string homepage = "")
    {
        // Use GitHub API to create a repository
        var newRepository = new NewRepository(repositoryName)
        {
            Homepage = homepage,
            Description = description,
            LicenseTemplate = Octokit.RepoSearchLicense.MIT.ToString(),
            HasProjects = false,
            HasIssues = true,
            HasWiki = false,
            HasDownloads = false,
            AutoInit = true,
            Private = false,
            AllowAutoMerge = true,
            Visibility = RepositoryVisibility.Public,
            IsTemplate = false,
            DeleteBranchOnMerge = true,
            AllowRebaseMerge = false,
            AllowMergeCommit = true,
            AllowSquashMerge = true,
            UseSquashPrTitleAsDefault = true
        };
        var repository = await _client.Repository.Create(newRepository);
    }
    
    public async Task UploadFileAsync(RepositoryReference repositoryReference, string filePath, string content, string commitMessage)
    {
        // Use GitHub API to upload a file
        var details = GitHubUrlDetailsExtractor.ExtractInfo(repositoryReference.FullName);
        var repository = await _client.Repository.Get(details.Username, details.RepositoryName);
        var branch = await _client.Git.Reference.Get(repository.Id, "heads/main");
        var latestCommit = await _client.Git.Commit.Get(repository.Id, branch.Object.Sha);
        // var tree = await client.Git.Tree.GetRecursive(repository.Id, latestCommit.Tree.Sha);
        var blob = new NewBlob
        {
            Encoding = EncodingType.Utf8,
            Content = content
        };
        var newBlob = await _client.Git.Blob.Create(repository.Id, blob);
        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Mode = "100644",
            Type = TreeType.Blob,
            Path = filePath,
            Sha = newBlob.Sha
        });
        var createdTree = await _client.Git.Tree.Create(repository.Id, newTree);
        var newCommit = new NewCommit(commitMessage, createdTree.Sha, latestCommit.Sha);
        var commit = await _client.Git.Commit.Create(repository.Id, newCommit);
        await _client.Git.Reference.Update(repository.Id, "heads/main", new ReferenceUpdate(commit.Sha));
    }
    
    public async Task<bool> CreatOrUpdateSecretAsync(RepositoryReference repositoryReference, string secretName, string secretValue)
    {
        // Use GitHub API to create or update a secret
        // var repository = await _client.Repository.Get(repositoryReference.Owner, repositoryReference.Name);
        // var secrets = await _client.Actions..Repository..Secret.GetAll(repository.Id);
        // var secret = secrets.FirstOrDefault(s => s.Name == secretName);
        // if (secret is null)
        // {
        //     await _client.Repository.Secret.Create(repository.Id, secretName, secretValue);
        //     return true;
        // }
        // else
        // {
        //     await _client.Repository.Secret.Update(repository.Id, secretName, secretValue);
        //     return false;
        // }
        return true;
    }

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