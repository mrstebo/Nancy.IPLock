using Moq;
using Nancy.Testing;
using NUnit.Framework;

namespace Nancy.IPLock.Tests
{
    [TestFixture]
    public class IPLockTests
    {
        private Mock<IIPValidator> _ipValidator;
        private Browser _browser;

        [SetUp]
        public void SetUp()
        {
            _ipValidator = new Mock<IIPValidator>();
            _browser = new Browser(config =>
            {
                config.Module<TestModule>();

                config.RequestStartup((container, pipelines, ctx) =>
                {
                    IPLock.Enable(pipelines, new IPLockConfiguration
                    {
                        IPValidator = _ipValidator.Object
                    });
                });
            });
        }

        [TearDown]
        public void TearDown()
        {
            _browser = null;
            _ipValidator = null;
        }

        [Test]
        public void ShouldReturn_Forbidden()
        {
            _ipValidator
                .Setup(x => x.IsValid("82.32.54.12"))
                .Returns(false);

            var response = _browser.Get("/", with =>
            {
                with.HttpRequest();
                with.UserHostAddress("82.32.54.12");
            });

            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Test]
        public void ShouldReturn_OK()
        {
            _ipValidator
                .Setup(x => x.IsValid("82.32.54.12"))
                .Returns(true);

            var response = _browser.Get("/", with =>
            {
                with.HttpRequest();
                with.UserHostAddress("82.32.54.12");
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private class TestModule : NancyModule
        {
            public TestModule()
                : base("/")
            {
                Get["/"] = _ => "Test Module";
            }
        }
    }
}
