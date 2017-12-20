namespace Cake.SmartAssembly
{
    /// <summary>
    /// Settings for SmartAssembly.
    /// </summary>
    public class SmartAssemblySettings: AutoToolSettings
    {
        /// <summary>
        /// Signing your assembly with a strong name key
        /// </summary>
        public string KeyFilename { get; set; }
        /// <summary>
        /// Adding tamper protection to the assembly
        /// </summary>
        public bool? TamperProtection { get; set; }
        /// <summary>
        /// Reducing the assembly's unused, allocated memory
        /// </summary>
        public bool? ReduceMem { get; set; }
        /// <summary>
        /// Sealing classes that are not inherited
        /// </summary>
        public bool? SealClasses { get; set; }
        /// <summary>
        /// Preventing Microsoft IL Disassembler from opening your assembly
        /// </summary>
        public bool? PreventILDasm { get; set; }
        /// <summary>
        /// Setting the types / methods name mangling level
        /// Apply types / methods name mangling at the specified level to assemblies with nameobfuscate:true:
        /// 1 = ASCII renaming
        /// 2 = Unicode unprintable characters
        /// 3 = Unicode unprintable characters and advanced renaming
        /// Note that this switch does not apply name mangling itself.It sets the level that will be used for assemblies with nameobfuscate:true set.
        /// </summary>
        public int? TypeMethodObfuscation { get; set; }
        /// <summary>
        /// Setting the fields name mangling level
        /// </summary>
        public int? FieldObfuscation { get; set; }
        /// <summary>
        /// Adding method parent obfuscation to the assembly
        /// </summary>
        public bool? MethodParentObfuscation { get; set; }
        /// <summary>
        /// Setting strings encoding
        /// To disable strings encoding:
        /// 
        /// /stringsencoding=false
        /// 
        /// To enable strings encoding, set /stringsencoding=true. You may specify settings for the three sub-options:
        /// 
        ///         Use improved protection?
        ///         Compress and encrypt resources?
        ///         Cache the strings for improved performance?
        /// 
        /// /stringsencoding=true;
        /// improved:[true | false],
        /// compressencrypt:[true | false]
        ///         cache:[true | false]
        /// </summary>
        public bool? StringsEncoding { get; set; }
        /// <summary>
        /// Creating a PDB file
        /// </summary>
        public bool? MakePdb { get; set; }
        /// <summary>
        /// Obfuscating URLs in the PDB file
        /// </summary>
        public bool? ObfuscatePdbUrls { get; set; }
        /// <summary>
        /// Setting the application name for use in error and feature usage reports
        /// </summary>
        public string ReportAppName { get; set; }
        /// <summary>
        /// Setting the project name for use in error and feature usage reports
        /// </summary>
        public string ReportProjectName { get; set; }
        /// <summary>
        /// Setting the company name for use in error and feature usage reports
        /// </summary>
        public string ReportCompanyName { get; set; }
        /// <summary>
        /// Enabling error reporting
        /// </summary>
        public bool? ErrorReportingTemplate { get; set; }
        /// <summary>
        /// Enabling feature usage reporting
        /// </summary>
        public string FeatureUsageTemplate { get; set; }
    }
}
