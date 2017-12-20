using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cake.SmartAssembly
{
    /// <summary>
    /// Arguments builder for per-assembly settings.
    /// </summary>
    public static class AssemblyArgumentsBuilderExtension
    {
        /// <summary>
        /// Appends all arguments from <paramref name="settings"/>.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public static string[] CollectAll(this AssemblyOptionSettings settings)
        {
            if (settings == null)
            {
                settings = new AssemblyOptionSettings();
            }
            var result = new List<string>();
            // ignore pre (Assembly attribute)
            //result.AddRange(AppendArguments(settings, preCommand: true));
            result.AddRange(AppendArguments(settings, preCommand: false));
            return result.ToArray();
        }
        /// <summary>
        /// Appends pre or post command arguments.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="preCommand"></param>
        public static string[] AppendArguments(AssemblyOptionSettings settings, bool preCommand)
        {
            var result = new List<string>();
            foreach (var property in typeof(AssemblyOptionSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                foreach (string argument in GetArgumentFromProperty(property, settings, preCommand: preCommand))
                {
                    if (!string.IsNullOrEmpty(argument))
                    {
                        result.Add(argument);
                    }
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Gets and processes <paramref name="property"/> value from <paramref name="settings"/>.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="settings">The settings.</param>
        /// <param name="preCommand">Pre or post command.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetArgumentFromProperty(PropertyInfo property, AssemblyOptionSettings settings, bool preCommand)
        {
            var autoPropertyAttribute = GetAutoPropertyAttributeOrNull(property);
            var parameterAttribute = GetParameterAttributeOrNull(property);
            if (autoPropertyAttribute?.Format != null)
            {
                if (autoPropertyAttribute.PreCommand == preCommand)
                {
                    yield return GetArgumentFromAutoProperty(autoPropertyAttribute, property, property.GetValue(settings));
                }
            }
            else if (!preCommand && (autoPropertyAttribute == null || !autoPropertyAttribute.PreCommand) || (autoPropertyAttribute != null && autoPropertyAttribute.PreCommand && preCommand))
            {
                if (property.PropertyType == typeof(bool))
                {
                    yield return GetArgumentFromBoolProperty(property, (bool)property.GetValue(settings));
                }
                else if (property.PropertyType == typeof(bool?))
                {
                    yield return GetArgumentFromNullableBoolProperty(property, (bool?)property.GetValue(settings), parameterAttribute);
                }
                else if (property.PropertyType == typeof(int?))
                {
                    yield return GetArgumentFromNullableIntProperty(property, (int?)property.GetValue(settings));
                }
                else if (property.PropertyType == typeof(Int64?))
                {
                    yield return GetArgumentFromNullableInt64Property(property, (Int64?)property.GetValue(settings));
                }
                else if (property.PropertyType == typeof(UInt64?))
                {
                    yield return GetArgumentFromNullableUInt64Property(property, (UInt64?)property.GetValue(settings));
                }
                else if (property.PropertyType == typeof(UInt16?))
                {
                    yield return GetArgumentFromNullableUInt16Property(property, (UInt16?)property.GetValue(settings));
                }
                else if (property.PropertyType == typeof(string))
                {
                    yield return GetArgumentFromStringProperty(property, (string)property.GetValue(settings), parameterAttribute);
                }
                else if (property.PropertyType == typeof(TimeSpan?))
                {
                    yield return GetArgumentFromNullableTimeSpanProperty(property, (TimeSpan?)property.GetValue(settings));
                }
                else if (property.PropertyType == typeof(string[]))
                {
                    foreach (string arg in GetArgumentFromStringArrayProperty(property, (string[])property.GetValue(settings)))
                    {
                        yield return arg;
                    }
                }
            }
        }
        /// <summary>
        /// Uses format specified in attribute to format the argument.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromAutoProperty(AutoPropertyAttribute attribute, PropertyInfo property, object value)
        {
            if (value == null)
            {
                return null;
            }

            string result = string.Format(attribute.Format, GetPropertyName(property.Name), value);
            if (attribute.OnlyWhenTrue)
            {
                bool boolvalue = (bool)value;
                return boolvalue ? result : string.Empty;
            }
            else
            {
                if (property.PropertyType == typeof(string[]))
                {
                    var strings = (string[])value;
                    result = string.Join(" ", strings.Select(s => string.Format(attribute.Format, GetPropertyName(property.Name), s)));
                }
            }
            return result;
        }
        /// <summary>
        /// Retrieve <see cref="AutoPropertyAttribute"/> from property or null if there isn't one.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static AutoPropertyAttribute GetAutoPropertyAttributeOrNull(PropertyInfo property)
        {
            return property.GetCustomAttribute<AutoPropertyAttribute>();
        }
        /// <summary>
        /// Retrieve <see cref="ParameterAttribute"/> from property or null if there isn't one.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static ParameterAttribute GetParameterAttributeOrNull(PropertyInfo property)
        {
            return property.GetCustomAttribute<ParameterAttribute>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromBoolProperty(PropertyInfo property, bool value)
        {
            return value ? $"{GetPropertyName(property.Name)}:{value.ToString().ToLower()}" : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromNullableIntProperty(PropertyInfo property, int? value)
        {
            return value.HasValue ? $"{GetPropertyName(property.Name)}:{value.Value}" : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromNullableInt64Property(PropertyInfo property, Int64? value)
        {
            return value.HasValue ? $"{GetPropertyName(property.Name)}:{value.Value}" : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromNullableUInt64Property(PropertyInfo property, UInt64? value)
        {
            return value.HasValue ? $"{GetPropertyName(property.Name)}:{value.Value}" : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromNullableUInt16Property(PropertyInfo property, UInt16? value)
        {
            return value.HasValue ? $"{GetPropertyName(property.Name)}:{value.Value}" : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromNullableBoolProperty(PropertyInfo property, bool? value, ParameterAttribute parameter)
        {
            if (value.HasValue)
            {
                return $"{GetPropertyName(property.Name)}:{value.ToString().ToLower()}";
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetArgumentFromDictionaryProperty(PropertyInfo property, Dictionary<string, string> values)
        {
            if (values != null)
            {
                foreach (var kp in values)
                {
                    yield return GetArgumentFromStringProperty(property, $"{kp.Key}={kp.Value}", null);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetArgumentFromStringArrayProperty(PropertyInfo property, string[] values)
        {
            if (values != null)
            {
                foreach (string value in values)
                {
                    yield return GetArgumentFromStringProperty(property, value, null);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetArgumentFromStringProperty(PropertyInfo property, string value, ParameterAttribute parameter)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return $"{GetPropertyName(property.Name)}:\"{value}\"";
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromNullableTimeSpanProperty(PropertyInfo property, TimeSpan? value)
        {
            return value.HasValue ? $"{GetPropertyName(property.Name)}:{ConvertTimeSpan(value.Value)}" : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ConvertTimeSpan(TimeSpan source)
        {
            return $"{Math.Floor(source.TotalHours)}h{source.Minutes}m{source.Seconds}s";
        }

        /// <summary>
        /// Converts property name to arguments format
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <example>NoForce -> no-force</example>
        public static string GetPropertyName(string name)
        {
            string result = null;
            if (!string.IsNullOrEmpty(name))
            {
                result = name.ToLower();
            }
            return result;
        }
    }
}
