using NUnit.Framework;

namespace DicePoker.Tests.EditMode
{
    public class ServiceLocatorTests
    {
        private class TestService
        {
            public string Data { get; set; } = "test";
        }

        [SetUp]
        public void Setup()
        {
            // ServiceLocator.Clear();
        }

        [Test]
        public void Register_And_Get_ReturnsRegisteredService()
        {
            // Arrange
            var service = new TestService { Data = "hello" };

            // Act
            ServiceLocator.Register(service);
            var retrieved = ServiceLocator.Get<TestService>();

            // Assert
            Assert.IsNotNull(retrieved);
            Assert.AreEqual("hello", retrieved.Data);
        }

        [Test]
        public void Get_WithoutRegistration_ThrowsException()
        {
            // Arrange & Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => { ServiceLocator.Get<TestService>(); });
        }

        [Test]
        public void Register_SameTypeTwice_ReplacesService()
        {
            // Arrange
            var service1 = new TestService { Data = "first" };
            var service2 = new TestService { Data = "second" };

            // Act
            ServiceLocator.Register(service1);
            ServiceLocator.Register(service2);
            var retrieved = ServiceLocator.Get<TestService>();

            // Assert
            Assert.AreEqual("second", retrieved.Data);
        }

        [Test]
        public void IsRegistered_ReturnsTrueForRegisteredService()
        {
            // Arrange
            var service = new TestService();

            // Act
            ServiceLocator.Register(service);

            // Assert
            Assert.IsTrue(ServiceLocator.IsRegistered<TestService>());
        }

        [Test]
        public void IsRegistered_ReturnsFalseForUnregisteredService()
        {
            // Assert
            Assert.IsFalse(ServiceLocator.IsRegistered<TestService>());
        }

        [Test]
        public void Clear_RemovesAllServices()
        {
            // Arrange
            var service = new TestService();
            ServiceLocator.Register(service);

            // Act
            ServiceLocator.Clear();

            // Assert
            Assert.IsFalse(ServiceLocator.IsRegistered<TestService>());
        }

        [Test]
        public void TryGet_WithRegisteredService_ReturnsTrue()
        {
            // Arrange
            var service = new TestService();
            ServiceLocator.Register(service);

            // Act
            bool success = ServiceLocator.TryGet<TestService>(out var retrieved);

            // Assert
            Assert.IsTrue(success);
            Assert.IsNotNull(retrieved);
        }

        [Test]
        public void TryGet_WithoutRegistration_ReturnsFalse()
        {
            // Act
            bool success = ServiceLocator.TryGet<TestService>(out var retrieved);

            // Assert
            Assert.IsFalse(success);
            Assert.IsNull(retrieved);
        }
    }
}
