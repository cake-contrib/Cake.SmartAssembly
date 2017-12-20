# Cake.SmartAssembly

A Cake AddIn that extends Cake with Redgate's [SmartAssembly](https://www.red-gate.com/products/dotnet-development/smartassembly/).

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)
[![NuGet](https://img.shields.io/nuget/v/Cake.SmartAssembly.svg)](https://www.nuget.org/packages/Cake.SmartAssembly)

## Requirements

Redgate's SmartAssembly has to be installed. Addin uses the latest version according to installation directory (**%ProgramFiles%/Red Gate/SmartAssembly***).

Runs only on Windows.

SmartAssembly [command line documentation](https://documentation.red-gate.com/sa6/building-your-assembly/using-the-command-line-mode)

## Including addin

Including addin in cake script is easy.
```c#
#addin "Cake.SmartAssembly"
```

## Usage

To use the addin just add it to Cake call the aliases and configure any settings you want.

```csharp
#addin "Cake.SmartAssembly"

...

Task("Create")
    .Does(() => {
            SmartAssemblyCreate(
                // sa project
                File("./test.saproj"),
                // input assembly
                File("./[path]\test.exe"), 
                // output assembly
                File("./[path]\test_sa.exe"), 
                new SmartAssemblySettings { TamperProtection = true });
    });
Task("Build")
    .Does(() => {
            SmartAssemblyBuild(
                // sa project
                File("./test.saproj"), 
                new SmartAssemblySettings { TamperProtection = true });
    });

```
## Credits

Brought to you by [Miha Markic](https://github.com/MihaMarkic) ([@MihaMarkic](https://twitter.com/MihaMarkic/)) and contributors.