#r "../src/Cake.SmartAssembly/bin/Debug/net46/Cake.SmartAssembly.dll"

var target = Argument("target", "Default");

Task("Create")
    .Does(() => {
            SmartAssemblyCreate(
                // sa project
                File("./test.saproj"),
                // input assemlby
                File("./IntegrationTestSample/IntegrationTestSample/bin/Debug/net46/IntegrationTestSample.exe"), 
                // output assembly
                File("./IntegrationTestSample/IntegrationTestSample/bin/Debug/net46/IntegrationTestSample_sa.exe"), 
                new SmartAssemblySettings { TamperProtection = true });
    });
Task("Build")
    .Does(() => {
            SmartAssemblyBuild(
                // sa project
                File("./test.saproj"), 
                new SmartAssemblySettings { TamperProtection = true });
    });

Task("PerAssembly")
    .Does(() => {
            SmartAssemblyBuild(
                // sa project
                File("./test.saproj"), 
                new SmartAssemblySettings { TamperProtection = true },
                new AssemblyOptionSettings { Assembly = "IntegrationTestSample", Prune = true });
    });

RunTarget(target);