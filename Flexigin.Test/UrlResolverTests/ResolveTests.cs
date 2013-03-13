using Flexigin.Core;
using NUnit.Framework;
using System;

namespace Flexigin.Test.UrlResolverTests
{
    [TestFixture]
    public class ResolveTests
    {
        private UrlResolver _resolver;

        [SetUp]
        public void BeforeEach()
        {
            _resolver = new UrlResolver();
        }

        [Test]
        public void Throws_An_Error_When_FileType_Is_Not_Supported()
        {
            var path = "/components/exe";

            Assert.Throws<NotSupportedException>(() => _resolver.Resolve(path));
        }

        [Test]
        public void Removes_QueryString()
        {
            var path = "/components/js?someQuerystring=1";

            var component = _resolver.Resolve(path);

            Assert.That(component.Path.Contains("?"), Is.False);
        }

        [Test]
        public void Resolves_JavaScript_FileType()
        {
            var path = "/components/js";

            var component = _resolver.Resolve(path);

            Assert.That(component.FileType, Is.EqualTo(FileType.JavaScript));
        }

        [Test]
        public void Resolves_StyleSheet_FileType()
        {
            var path = "/components/css";

            var component = _resolver.Resolve(path);

            Assert.That(component.FileType, Is.EqualTo(FileType.StyleSheet));
        }

        [Test]
        public void Resolves_Html_FileType()
        {
            var path = "/components/html";

            var component = _resolver.Resolve(path);

            Assert.That(component.FileType, Is.EqualTo(FileType.Html));
        }

        [Test]
        public void Removes_Start_Slash()
        {
            var path = "/components/js";

            var component = _resolver.Resolve(path);

            Assert.That(component.Path, Is.EqualTo("components/"));
        }

        [Test]
        public void Adds_Missing_End_Slash()
        {
            var path = "components/js";

            var component = _resolver.Resolve(path);

            Assert.That(component.Path, Is.EqualTo("components/"));
        }

        [Test]
        public void Ignores_CamelCase()
        {
            var path = "cOMpOnents/JS";

            var component = _resolver.Resolve(path);

            Assert.That(component.Path, Is.EqualTo("components/"));
        }
    }
}
