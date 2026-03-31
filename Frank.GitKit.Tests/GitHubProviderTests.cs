using Frank.GitKit;
using Frank.GitKit.Providers.GitHub;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Octokit;
using Xunit.Abstractions;

namespace Frank.GitKit.Tests;

public class GitHubProviderTests
{
    private readonly ITestOutputHelper _outputHelper;

    public GitHubProviderTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task CloneRepositoryAsync()
    {
        // Arrange
        var provider = new GitHubProvider(new GitHubClient(new Connection(new ProductHeaderValue("GitKit"), new Uri("https://api.github.com/"))));
        var tempOutputPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), "Frank.Reflection");
        
        // Act
        await provider.CloneRepositoryAsync(new Uri("https://github.com/frankhaugen/Frank.Reflection.git"), new DirectoryInfo(tempOutputPath));

        // Assert
        Assert.True(Directory.Exists(tempOutputPath));
        
        _outputHelper.WriteLine($"Repository cloned to {tempOutputPath}");
        _outputHelper.WriteLine($"Files in repository:\n{string.Join(",\n", Directory.GetFiles(tempOutputPath, "*", SearchOption.AllDirectories).Order())}");
    }
}