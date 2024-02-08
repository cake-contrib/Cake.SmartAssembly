#load nuget:?package=Cake.Recipe&version=3.1.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context, 
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.SmartAssembly",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.SmartAssembly",
    appVeyorAccountName: "cakecontrib",
	shouldPostToGitter: false,
	shouldRunInspectCode: false,
	uldRunCodecov: false,
	shouldRunDotNetCorePack: true,
	shouldRunCoveralls: false); // Disabled because it's currently failing

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    testCoverageFilter: "+[*]* -[nunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");
Build.RunDotNetCore();
