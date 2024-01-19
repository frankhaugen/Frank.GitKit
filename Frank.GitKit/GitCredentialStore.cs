using Frank.Reflection;
using Microsoft.Extensions.Configuration;

namespace Frank.GitKit;

public class GitCredentialStore(IConfiguration configuration) : IGitCredentialStore
{
    public string? GetCredentials<TProvider>(string? fieldName = null) where TProvider : IRemoteGitProvider
    {
        var providerName = typeof(TProvider).GetDisplayName();
        var key = $"GitKit.{providerName}.Credentials";
        if (fieldName != null)
            key += $":{fieldName}";
        return configuration[key];
    }
}