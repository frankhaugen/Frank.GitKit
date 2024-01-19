namespace Frank.GitKit;

/// <summary>
/// Represents a reference to a repository. This is used to identify a repository on a remote git provider.
/// </summary>
public record RepositoryReference(string Owner, string Name);