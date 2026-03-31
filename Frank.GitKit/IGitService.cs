namespace Frank.GitKit;

public interface IGitService
{
    Task CloneRepositoryAsync(Uri remoteRepository, DirectoryInfo localDirectory, string branch = "main");
}

public class GitService : IGitService
{
    /// <inheritdoc />
    public async Task CloneRepositoryAsync(Uri remoteRepository, DirectoryInfo localDirectory, string branch = "main")
    {
        
    }
}
