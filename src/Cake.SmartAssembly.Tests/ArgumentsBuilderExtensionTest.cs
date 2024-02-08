using NUnit.Framework;
using System.Reflection;

namespace Cake.SmartAssembly.Tests
{
    public class ArgumentsBuilderExtensionTest
    {
        public static PropertyInfo StringProperty => GetProperty(nameof(TestSettings.String));
        public static PropertyInfo NullableBoolProperty => GetProperty(nameof(TestSettings.NullableBool));
        public static PropertyInfo NullableIntProperty => GetProperty(nameof(TestSettings.NullableInt));
        public static PropertyInfo GetProperty(string name)
        {
            return typeof(TestSettings).GetProperty(name, BindingFlags.Public | BindingFlags.Instance)!;
        }
        [TestFixture]
        public class GetArgumentFromNullableBoolProperty
        {
            [Test]
            public void WhenTrueAndNoParameter_FormatsProperly()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, true, parameter: null);

                Assert.That(actual, Is.EqualTo("/nullablebool=true"));
            }
            [Test]
            public void WhenFalseNoParameter_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, false, parameter: null);

                Assert.That(actual, Is.EqualTo("/nullablebool=false"));
            }
            [Test]
            public void WhenNullNoParameter_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, null, parameter: null);

                Assert.That(actual, Is.Null);
            }
        }
        [TestFixture]
        public class GetArgumentFromStringProperty
        {
            [Test]
            public void WhenGivenStringPropertyAndNoParameter_FormatsProperly()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, "tubo", parameter: null);

                Assert.That(actual, Is.EqualTo("/string=\"tubo\""));
            }
            [Test]
            public void WhenGivenNullAndNoParameter_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, null, parameter: null);

                Assert.That(actual, Is.Null);
            }
        }
        [TestFixture]
        public class GetArgumentFromIntProperty
        {
            [Test]
            public void WhenGivenValue_FormatsProperly()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableIntProperty, 1);

                Assert.That(actual, Is.EqualTo("/nullableint=1"));
            }
            [Test]
            public void WhenGivenNull_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(StringProperty, null);

                Assert.That(actual, Is.Null);
            }
        }

        [TestFixture]
        public class GetPropertyName
        {
            [TestCase("Name", ExpectedResult = "name")]
            [TestCase("NameExtended", ExpectedResult = "nameextended")]
            public string? WhenInput_ReturnsCorrectlyFormatted(string name)
            {
                return ArgumentsBuilderExtension.GetPropertyName(name);
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
