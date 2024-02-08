# Cake.SmartAssembly

A Cake AddIn that extends Cake with Redgate's [SmartAssembly](https://www.red-gate.com/products/dotnet-development/smartassembly/).

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)
[![NuGet](https://img.shields.io/nuget/v/Cake.SmartAssembly.svg)](https://www.nuget.org/packages/Cake.SmartAssembly)

|Branch|Status|
|------|------|
|Master|[![Build status](https://ci.appveyor.com/api/projects/status/github/cake-contrib/Cake.SmartAssembly?branch=master&svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-smartassembly)|
|Develop|[![Build status](https://ci.appveyor.com/api/projects/status/github/cake-contrib/Cake.SmartAssembly?branch=develop&svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-smartassembly)|

## Important

1.5.0 
* References Cake 4.0.0
* Drops support for .NET Framework
* Supports .net 6+

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

## Discussion

If you have questions, search for an existing one, or create a new discussion on the Cake GitHub repository, using the `extension-q-a` category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions/categories/extension-q-a)

## Credits

Brought to you by [Miha Markic](https://github.com/MihaMarkic) and contributors. 

![Mastodon Follow](https://img.shields.io/mastodon/follow/001030236)