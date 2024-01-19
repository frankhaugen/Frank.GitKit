namespace Frank.GitKit;

public interface IGitProvider
{
    Task CloneRepositoryAsync(string repositoryUrl, string localPath);
    Task CommitAsync(string message);
    Task PushAsync();
    Task<IEnumerable<string>> GetBranchesAsync();
}