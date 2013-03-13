using Flexigin.Core;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;

namespace Flexigin.Test.ComponentLoaderTests
{
    [TestFixture]
    public class GetComponentTests
    {
        private ComponentLoader _loader;
        private readonly string _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../TestData/Components/");

        [SetUp]
        public void BeforeEach()
        {
            _loader = new ComponentLoader();
        }

        [Test]
        public void Returns_StatusCode_OK_When_The_Directory_Was_Found()
        {
            var path = "/User/js";

            var component = _loader.GetComponent(_basePath, path);

            Assert.That(component.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Returns_StatusCode_NotFound_When_The_Directory_Does_Not_Exist()
        {
            var path = "/iDoNotExist/User/js";

            var component = _loader.GetComponent(_basePath, path);

            Assert.That(component.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void Can_Return_Javascript()
        {
            var path = "/User/js";

            var component = _loader.GetComponent(_basePath, path);

            Assert.That(component.Content, Is.Not.Null);
            Assert.That(component.Content.Contains("var user"), Is.True);
        }

        [Test]
        public void Can_Return_Html()
        {
            var path = "/User/html";

            var component = _loader.GetComponent(_basePath, path);

            Assert.That(component.Content, Is.Not.Null);
            Assert.That(component.Content.Contains("<div id=\"user\">"), Is.True);
        }

        [Test]
        public void Can_Return_Css()
        {
            var path = "/User/css";

            var component = _loader.GetComponent(_basePath, path);

            Assert.That(component.Content, Is.Not.Null);
            Assert.That(component.Content.Contains("margin: 7px;"), Is.True);
        }

        [Test]
        public void Does_Not_Include_SubDirectories()
        {
            var path = "/User/js";

            var component = _loader.GetComponent(_basePath, path, false);

            Assert.That(component.Content, Is.Not.Null);
            Assert.That(component.Content.Contains("var userProfile"), Is.False);
        }

        [Test]
        public void Can_Traverse_Directory_Tree()
        {
            var path = "/User/js";

            var component = _loader.GetComponent(_basePath, path, true);

            Assert.That(component.Content, Is.Not.Null);
            Assert.That(component.Content.Contains("var userProfile"), Is.True);
        }

        [Test]
        public void Returns_A_Corrent_JavaScript_ContentType()
        {
            var path = "/User/js";

            var component = _loader.GetComponent(_basePath, path);

            Assert.That(component.ContentType, Is.EqualTo("application/x-javascript"));
        }

        [Test]
        public void Returns_A_Corrent_Css_ContentType()
        {
            var path = "/User/css";

            var component = _loader.GetComponent(_basePath, path);

            Assert.That(component.ContentType, Is.EqualTo("text/css"));
        }

        [Test]
        public void Returns_A_Corrent_Html_ContentType()
        {
            var path = "/User/html";

            var component = _loader.GetComponent(_basePath, path);

            Assert.That(component.ContentType, Is.EqualTo("text/html"));
        }
    }
}
