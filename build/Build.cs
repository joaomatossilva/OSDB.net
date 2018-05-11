using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.BuildServers;
using Nuke.Common.Git;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    // Console application entry point. Also defines the default target.
    public static int Main () => Execute<Build>(x => x.Test);

    // Auto-injection fields:

    // [GitVersion] readonly GitVersion GitVersion;
    // Semantic versioning. Must have 'GitVersion.CommandLine' referenced.

    // [GitRepository] readonly GitRepository GitRepository;
    // Parses origin, branch name and head from git config.

    // [Parameter] readonly string MyGetApiKey;
    // Returns command-line arguments and environment variables.

    public bool IsTagged => AppVeyor.Instance?.RepositoryTag ?? false;

    int Revision => AppVeyor.Instance?.BuildNumber ?? 1;

    public string RevisionString => IsTagged ? null : $"rev{Revision:D4}";

    Target Clean => _ => _
            .OnlyWhen(() => false) // Disabled for safety.
            .Executes(() =>
            {
                DeleteDirectories(GlobDirectories(SourceDirectory, "**/bin", "**/obj"));
                EnsureCleanDirectory(OutputDirectory);
            });

    Target Restore => _ => _
            .DependsOn(Clean)
            .Executes(() =>
            {
                DotNetRestore(s => DefaultDotNetRestore);
            });

    Target Compile => _ => _
            .DependsOn(Restore)
            .Executes(() =>
            {
                DotNetBuild(s => DefaultDotNetBuild
                    .SetProjectFile(RootDirectory / "OSDBnet"));
            });

    Target Pack => _ => _
        .DependsOn(Compile, Test)
        .Executes(() =>
        {
            DotNetPack(s => DefaultDotNetPack
                .SetOutputDirectory(ArtifactsDirectory)
                .SetProject(RootDirectory / "OSDBnet")
                .SetVersionSuffix(RevisionString));
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(RootDirectory / "OSDBnet.Tests"));
        });
}
