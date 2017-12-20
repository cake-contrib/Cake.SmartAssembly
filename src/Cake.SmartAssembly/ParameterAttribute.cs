using System;

namespace Cake.SmartAssembly
{
    /// <summary>
    /// Decorates with parameter name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Parameter name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        public ParameterAttribute(string name)
        {
            Name = name;
        }
    }
}
