using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;

namespace Cake.SmartAssembly
{
    /// <summary>
    /// 
    /// </summary>
    public class SmartAssemblyTool<TSettings> : Tool<TSettings>
        where TSettings : AutoToolSettings, new()
    {
        readonly IFileSystem _fileSystem;
        readonly ICakeEnvironment _environment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="environment"></param>
        /// <param name="processRunner"></param>
        /// <param name="tools"></param>
        public SmartAssemblyTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>The name of the tool.</returns>
        protected override string GetToolName()
        {
            return "SmartAssembly";
        }

        /// <summary>
        /// Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "SmartAssembly.com" };
        }
        /// <summary>
        /// Finds the proper SmartAssembly executable path.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>A single path to SmartAssembly executable.</returns>
        protected override IEnumerable<FilePath> GetAlternativeToolPaths(TSettings settings)
        {
            var path = SmartAssemblyResolver.GetSmartAssemblyPath(_fileSystem, _environment);
            if (path != null)
            {
                return new FilePath[] { path };
            }
            else
            {
                return new FilePath[0];
            }
        }
        /// <summary>
        /// Runs using given <paramref name=" settings"/> and per-assembly settings <paramref name="args"/>.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="settings"></param>
        /// <param name="args"></param>
        public void Run(string command, TSettings settings, AssemblyOptionSettings[] args)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException(nameof(command));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            Run(settings, GetArguments(command, settings, args));
        }
        /// <summary>
        /// Runs given command
        /// </summary>
        /// <param name="command"></param>
        public void Run(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException(nameof(command));
            }
            Run(new TSettings(), new ProcessArgumentBuilder().Append(command));
        }
        /// <summary>
        /// Creates arguments for the tool.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="settings"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ProcessArgumentBuilder GetArguments(string command, TSettings settings, AssemblyOptionSettings[] args)
        {
            var builder = new ProcessArgumentBuilder();
            builder.Append(command);
            builder.AppendAll(settings);
            foreach (var arg in args)
            {
                var arguments = arg.CollectAll();
                var text = string.Join(",", arguments);
                builder.Append($"/assembly=\"{arg.Assembly}\"{(arguments.Length > 0 ? ";": "")}" + text);
            }
            return builder;
        }
    }
}
