using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using System;

namespace Cake.SmartAssembly
{
    static partial class SmartAssemblyAliases
    {
        /// <summary>
        /// Creates a new SmartAssembly project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The SmartAssembly project.</param>
        /// <param name="input">The input assembly.</param>
        /// <param name="output">The output assembly.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="args">The per-assembly settings.</param>
        [CakeMethodAlias]
        public static void SmartAssemblyCreate(this ICakeContext context, FilePath project, FilePath input, FilePath output, SmartAssemblySettings settings, 
            params AssemblyOptionSettings[] args)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            var runner = new SmartAssemblyTool<SmartAssemblySettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run($"/create {project} /input={input} /output={output}", settings, args ?? new AssemblyOptionSettings[0]);
        }
        /// <summary>
        /// Builds a SmartAssembly project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The SmartAssembly project.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="args">The per-assembly settings.</param>
        [CakeMethodAlias]
        public static void SmartAssemblyBuild(this ICakeContext context, FilePath project, SmartAssemblySettings settings, params AssemblyOptionSettings[] args)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            var runner = new SmartAssemblyTool<SmartAssemblySettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run($"/build {project}", settings, args ?? new AssemblyOptionSettings[0]);
        }
        /// <summary>
        /// Edits a SmartAssembly project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The SmartAssembly project.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="args">The per-assembly settings.</param>
        [CakeMethodAlias]
        public static void SmartAssemblyEdit(this ICakeContext context, FilePath project, SmartAssemblySettings settings, params AssemblyOptionSettings[] args)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            var runner = new SmartAssemblyTool<SmartAssemblySettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run($"/edit {project}", settings, args ?? new AssemblyOptionSettings[0]);
        }
        /// <summary>
        /// Compacts SmartAssembly database.
        /// </summary>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        public static void SmartAssemblyCompactDB(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var runner = new SmartAssemblyTool<EmptySettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run("/compactdb");
        }
        /// <summary>
        /// Downloads SmartAssembly reports.
        /// </summary>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        public static void SmartAssemblyDownloadReports(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var runner = new SmartAssemblyTool<EmptySettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run("/downloadnewreports");
        }
        /// <summary>
        /// Adds SmartAssembly report.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="encryptedReport">Encrypted report file.</param>
        [CakeMethodAlias]
        public static void SmartAssemblyAddReport(this ICakeContext context, FilePath encryptedReport)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var runner = new SmartAssemblyTool<EmptySettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run($"/addreport {encryptedReport}");
        }
    }
}
