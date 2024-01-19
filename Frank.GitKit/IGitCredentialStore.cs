namespace Frank.GitKit;

public interface IGitCredentialStore
{
    string? GetCredentials<TProvider>(string? fieldName = null) where TProvider : IRemoteGitProvider;
}