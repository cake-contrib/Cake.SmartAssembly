namespace Cake.SmartAssembly
{
    /// <summary>
    /// Per assembly settings.
    /// </summary>
    public class AssemblyOptionSettings
    {
        /// <summary>
        /// The name of the assembly.
        /// </summary>
        [AutoProperty(PreCommand = true)]
        public string Assembly { get; set; }
        /// <summary>
        /// Enable / Disable pruning
        /// </summary>
        public bool? Prune { get; set; }
        /// <summary>
        /// Disable / Enable dependencies merging
        /// (Note that you cannot merge the main assembly.)
        /// </summary>
        public bool? Merge { get; set; }
        /// <summary>
        /// Disable / Enable dependencies embedding
        /// This option is ignored if merge:true.
        /// (Note that you cannot embed the main assembly.)
        /// </summary>
        public bool? Embed { get; set; }
        /// <summary>
        /// Enable / Disable types and methods obfuscation and field names obfuscation
        /// The obfuscation is applied at the levels specified for the project.
        /// </summary>
        public bool? NameObfuscate { get; set; }
        /// <summary>
        /// Enable / Disable the references dynamic proxy
        /// </summary>
        public bool? DynamicProxy { get; set; }
        /// <summary>
        /// Enable / Disable resources compression and encryption
        /// </summary>
        public bool? CompressEncryptResources { get; set; }
        /// <summary>
        /// Sets the level of control flow obfuscation to apply to the assembly:
        ///
        ///        False = None
        ///    0 = Use attributes to set Control Flow Obfuscation, see Obfuscating the control flow
        ///    1 = Basic
        ///    2 = Strictly valid
        ///    3 = Strongest
        ///    4 = Unverifiable
        /// </summary>
        public int? ControlFlowObfuscate { get; set; }
        /// <summary>
        /// Enable / Disable compression when the assembly is embedded.
        /// This option is ignored unless embed:true.
        /// </summary>
        public bool? CompressAssembly { get; set; }
        /// <summary>
        /// Whether you want to encrypt the assembly when it is embedded.
        /// This option is ignored unless embed:true and compressassembly:true
        /// </summary>
        public bool? EncryptAssembly { get; set; }
    }
}
