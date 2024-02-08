using NUnit.Framework;
using System.Reflection;

namespace Cake.SmartAssembly.Tests
{
    public class AssemblyArgumentsBuilderExtensionTest
    {
        public static PropertyInfo StringProperty => GetProperty(nameof(TestSettings.String));
        public static PropertyInfo NullableBoolProperty => GetProperty(nameof(TestSettings.NullableBool));
        public static PropertyInfo NullableIntProperty => GetProperty(nameof(TestSettings.NullableInt));
        public static PropertyInfo GetProperty(string name)
        {
            return typeof(TestSettings).GetProperty(name, BindingFlags.Public | BindingFlags.Instance)!;
        }
        [TestFixture]
        public class GetArgumentFromNullableBoolProperty: AssemblyArgumentsBuilderExtensionTest
        {
            [Test]
            public void WhenTrueAndNoParameter_FormatsProperly()
            {
                var actual = AssemblyArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, true, parameter: null);

                Assert.That(actual, Is.EqualTo("nullablebool:true"));
            }
            [Test]
            public void WhenFalseNoParameter_NullIsReturned()
            {
                var actual = AssemblyArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, false, parameter: null);

                Assert.That(actual, Is.EqualTo("nullablebool:false"));
            }
            [Test]
            public void WhenNullNoParameter_NullIsReturned()
            {
                var actual = AssemblyArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, null, parameter: null);

                Assert.That(actual, Is.Null);
            }
        }
        [TestFixture]
        public class GetArgumentFromStringProperty: AssemblyArgumentsBuilderExtensionTest
        {
            [Test]
            public void WhenGivenStringPropertyAndNoParameter_FormatsProperly()
            {
                var actual = AssemblyArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, "tubo", parameter: null);

                Assert.That(actual, Is.EqualTo("string:\"tubo\""));
            }
            [Test]
            public void WhenGivenNullAndNoParameter_NullIsReturned()
            {
                var actual = AssemblyArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, null, parameter: null);

                Assert.That(actual, Is.Null);
            }
        }
        [TestFixture]
        public class GetArgumentFromIntProperty: AssemblyArgumentsBuilderExtensionTest
        {
            [Test]
            public void WhenGivenValue_FormatsProperly()
            {
                var actual = AssemblyArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableIntProperty, 1);

                Assert.That(actual, Is.EqualTo("nullableint:1"));
            }
            [Test]
            public void WhenGivenNull_NullIsReturned()
            {
                var actual = AssemblyArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(StringProperty, null);

                Assert.That(actual, Is.Null);
            }
        }

        [TestFixture]
        public class GetPropertyName: AssemblyArgumentsBuilderExtensionTest
        {
            [TestCase("Name", ExpectedResult = "name")]
            [TestCase("NameExtended", ExpectedResult = "nameextended")]
            public string? WhenInput_ReturnsCorrectlyFormatted(string name)
            {
                return AssemblyArgumentsBuilderExtension.GetPropertyName(name);
            }
        }
        [TestFixture]
        public class AppendArguments: AssemblyArgumentsBuilderExtensionTest
        {
            [Test]
            public void WhenOnlyMergeSet_ReturnsCorrect()
            {
                var settings = new AssemblyOptionSettings { Merge = true };

                var actual = AssemblyArgumentsBuilderExtension.AppendArguments(settings, preCommand: false);

                Assert.That(actual, Is.EqualTo(new [] { "merge:true" }));
            }
        }
        [TestFixture]
        public class CollectAll : AssemblyArgumentsBuilderExtensionTest
        {
            [Test]
            public void WhenOnlyAssemblyNameSet_ReturnsWithoutIt()
            {
                var settings = new AssemblyOptionSettings { Assembly = "tubo.dll"};

                var actual = AssemblyArgumentsBuilderExtension.CollectAll(settings);

                Assert.That(actual, Is.Empty);
            }
        }

        public class TestSettings : AutoToolSettings
        {
            public string? String { get; set; }
            public bool? NullableBool { get; set; }
            public int? NullableInt { get; set; }
        }
    }
}
