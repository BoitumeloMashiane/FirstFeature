using Microsoft.Extensions.Logging.Abstractions;
using Services.Impl;
using FluentAssertions;
using Services.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private FakeOrderRepository _fakeOrderRepository;
        private FakeEmailClient _fakeEmailClient;
        private NotificationService _notificationService;
        private OrderService _service;

        [SetUp]
        public void SetUp()
        {
            _fakeOrderRepository = new FakeOrderRepository();
            _fakeEmailClient = new FakeEmailClient();

            var notificationLogger = NullLogger<NotificationService>.Instance;
            _notificationService = new NotificationService(_fakeEmailClient, notificationLogger);

            var orderLogger = NullLogger<OrderService>.Instance;
            _service = new OrderService(_fakeOrderRepository, _notificationService, orderLogger);
        }

        [Test]
        public async Task CreateOrderAsync_ValidInput_ShouldCreateOrder()
        {
            var customerId = Guid.NewGuid();
            var email = "customer@test.com";
            var productName = "Laptop";
            var amount = 999.99m;

            // Act
            var result = await _service.CreateOrderAsync(customerId, email, productName, amount);

            // Assert
            result.Should().NotBeNull();
            result.CustomerId.Should().Be(customerId);
            result.CustomerEmail.Should().Be(email);
            result.ProductName.Should().Be(productName);
            result.Amount.Should().Be(amount);
        }

        [Test]
        public async Task CreateOrderAsync_ValidInput_ShouldSaveOrder()
        {
            var customerId = Guid.NewGuid();

            // Act
            await _service.CreateOrderAsync(customerId, "test@test.com", "Product", 100m);

            // Assert
            _fakeOrderRepository.SavedOrders.Should().HaveCount(1);
            _fakeOrderRepository.SavedOrders[0].CustomerId.Should().Be(customerId);
        }

        [Test]
        public async Task CreateOrderAsync_ValidInput_ShouldSendNotification()
        {
            var email = "customer@test.com";
            var productName = "Laptop";
            var amount = 999.99m;

            // Act
            await _service.CreateOrderAsync(Guid.NewGuid(), email, productName, amount);

            // Assert
            _fakeEmailClient.SentEmails.Should().HaveCount(1);
            var sentEmail = _fakeEmailClient.SentEmails[0];
            sentEmail.To.Should().Be(email);
            sentEmail.Subject.Should().Be("Order Confirmation");
            sentEmail.Body.Should().Contain(productName);
            sentEmail.Body.Should().Contain(amount.ToString());
        }

        [Test]
        public async Task CreateOrderAsync_MultipleOrders_ShouldCreateAll()
        {
            // Act
            await _service.CreateOrderAsync(Guid.NewGuid(), "user1@test.com", "Product1", 10m);
            await _service.CreateOrderAsync(Guid.NewGuid(), "user2@test.com", "Product2", 20m);

            // Assert
            _fakeOrderRepository.SavedOrders.Should().HaveCount(2);
            _fakeEmailClient.SentEmails.Should().HaveCount(2);
        }
    }
}
