using Cake.Core;
using Cake.Core.IO;
using System;
using System.Linq;

namespace Cake.SmartAssembly
{
    /// <summary>
    /// SmartAssembly tool resolver.
    /// </summary>
    public class SmartAssemblyResolver
    {
        /// <summary>
        /// Returns the path of the latest SmartAssembly.com version.
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="environment"></param>
        /// <returns>The path of the latest SmartAssembly.com version</returns>
        /// <remarks>Throws if SmartAssembly isn't found.</remarks>
        public static FilePath GetSmartAssemblyPath(IFileSystem fileSystem, ICakeEnvironment environment)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException("fileSystem");
            }
            if (environment == null)
            {
                throw new ArgumentNullException("environment");
            }
            var programFiles = new DirectoryPath(Environment.GetEnvironmentVariable("ProgramFiles")).Combine("Red Gate");
            Console.WriteLine($"program files: {programFiles}");
            var query = from p in fileSystem.GetDirectory(programFiles).GetDirectories("SmartAssembly*", SearchScope.Current)
                        where fileSystem.Exist(p.Path.CombineWithFilePath("SmartAssembly.com"))
                        let name = p.Path.GetDirectoryName()
                        let vt = name.Split(' ')[1]
                        let version = int.Parse(vt)
                        orderby version descending
                        select p;
            return query.FirstOrDefault()?.Path.CombineWithFilePath("SmartAssembly.com");
        }
    }
}
