namespace Frank.GitKit.Providers.GitHub;

public class GitHubUrlDetailsExtractor
{
    public static (string Username, string RepositoryName) ExtractInfo(string url)
    {
        var uri = new Uri(url);
        string path = uri.AbsolutePath.TrimEnd('/'); // Trim trailing slashes
        string[] segments = path.Split('/');

        if (segments.Length >= 2)
        {
            string repoName = segments[2]; // Adjust index if necessary
            repoName = repoName.EndsWith(".git") ? repoName.Substring(0, repoName.Length - 4) : repoName;
            return (segments[1], repoName);
        }

        throw new ArgumentException("Invalid GitHub URL", nameof(url));
    }
}