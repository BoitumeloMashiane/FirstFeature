using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Services.Impl;
using Services.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Tests
{
    [TestFixture]
    public class NotificationServiceTests
    {
        private FakeEmailClient _fakeEmailClient;
        private NotificationService _service;

        [SetUp]
        public void SetUp()
        {
            _fakeEmailClient = new FakeEmailClient();
            var logger = NullLogger<NotificationService>.Instance;
            _service = new NotificationService(_fakeEmailClient, logger);
        }

        [Test]
        public async Task SendAsync_Should_Send_Email_Using_Email_Client()
        {
            var to = "user@test.com";
            var subject = "Test Subject";
            var body = "Hello World";

            // Act
            await _service.SendAsync(to, subject, body);

            // Assert
            _fakeEmailClient.SentEmails.Should().HaveCount(1);
            var email = _fakeEmailClient.SentEmails[0];
            email.To.Should().Be(to);
            email.Subject.Should().Be(subject);
            email.Body.Should().Be(body);
        }

        [Test]
        public async Task SendAsync_MultipleEmails_ShouldRecordAll()
        {
            // Act
            await _service.SendAsync("user1@test.com", "Subject 1", "Body 1");
            await _service.SendAsync("user2@test.com", "Subject 2", "Body 2");

            // Assert
            _fakeEmailClient.SentEmails.Should().HaveCount(2);
            _fakeEmailClient.SentEmails[0].To.Should().Be("user1@test.com");
            _fakeEmailClient.SentEmails[1].To.Should().Be("user2@test.com");
        }

        [Test]
        public void SendAsync_EmptyRecipient_ShouldThrow()
        {
            var emptyRecipient = "";

            // Act
            Func<Task> act = async () => await _service.SendAsync(emptyRecipient, "Subject", "Body");

            // Assert
            act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*Recipient email cannot be empty*");
        }

        [Test]
        public void SendAsync_NullRecipient_ShouldThrow()
        {
            // Act
            Func<Task> act = async () => await _service.SendAsync(null, "Subject", "Body");

            // Assert
            act.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public void SendAsync_EmptySubject_ShouldThrow()
        {
            // Act
            Func<Task> act = async () => await _service.SendAsync("user@test.com", "", "Body");

            // Assert
            act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*Subject cannot be empty*");
        }
    }
}
